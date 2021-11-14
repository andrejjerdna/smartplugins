using AxCoDesign.Applications.Library;
using AxCoDesign.ML.Library.AutoConnect;
using AxCoDesign.Plugins.Model;
using AxCoDesign.Plugins.Model.Model.Extensions;
using AxCoDesign.Plugins.Model.TeklaModelObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;

namespace AxCoDesign.Applications.AutoConnect.Model
{
    public class TeklaConnectCreator
    {
        private TeklaModel _teklaModel;
        private IModelData _data;
        IEnumerable<Part> _parts;
        TeklaPluginRules _pluginRules;
        
        public Guid ConnectGuid { get; private set; }

        public TeklaConnectCreator(TeklaModel teklaModel, IModelData data, IEnumerable<Part> parts, TeklaPluginRules pluginRules)
        {
            _teklaModel = teklaModel;
            _data = data;
            _parts = parts;
            _pluginRules = pluginRules;
        }

        public bool Insert()
        {
            try
            {
                if (_pluginRules.NameComponent == "")
                    return false;

                var mainParts = new List<Part>();
                var secondaryParts = new List<Part>();

                var part1 = _teklaModel.CurrentModel.SelectModelObject(new Tekla.Structures.Identifier(_data.GUID)) as Part;

                if (_pluginRules.PluginRulesType == PluginRulesEnum.MAINPART)
                {
                    mainParts.Add(part1);

                    mainParts.AddRange(_parts.Where(p => p.Identifier.GUID != part1.Identifier.GUID)
                        //.Where(p => p.Profile.ProfileString == part1.Profile.ProfileString)
                        .Where(p => p.Class == part1.Class)
                        .Take(_pluginRules.MainParts.Count()-1)
                        .OrderByDescending(p => p.GetPropertyDouble("WEIGHT"))
                        .ToList());

                    var secPart = _pluginRules.SecondaryParts.FirstOrDefault();

                    if (secPart != null)
                        secondaryParts = _parts.Where(p => p.Identifier.GUID != part1.Identifier.GUID)
                        //.Where(p => p.Profile.ProfileString == secPart.Profile.ProfileString)
                        .Where(p => p.Class == secPart.Class)
                        ?.Take(_pluginRules.SecondaryParts.Count())
                        .OrderByDescending(p => p.GetPropertyDouble("WEIGHT"))
                        .ToList();
                }
                else
                {
                    secondaryParts.Add(part1);

                    secondaryParts.AddRange(_parts.Where(p => p.Identifier.GUID != part1.Identifier.GUID)
                        //.Where(p => p.Profile.ProfileString == part1.Profile.ProfileString)
                        .Where(p => p.Class == part1.Class)
                        .Take(_pluginRules.SecondaryParts.Count() - 1)
                        .OrderByDescending(p => p.GetPropertyDouble("WEIGHT"))
                        .ToList());

                    var mainPart = _pluginRules.MainParts.FirstOrDefault();

                    if(mainPart != null)
                    mainParts = _parts.Where(p => p.Identifier.GUID != part1.Identifier.GUID)
                        //.Where(p => p.Profile.ProfileString == mainPart.Profile.ProfileString)
                        .Where(p => p.Class == mainPart.Class)
                        .Take(_pluginRules.MainParts.Count())
                        .OrderByDescending(p => p.GetPropertyDouble("WEIGHT"))
                        .ToList();
                }

                InsertComponent(mainParts, secondaryParts, _pluginRules);

                return true;
            }
            catch
            {
                return false;
            }
        }


        private void InsertComponent(IEnumerable<Part> mainPart, IEnumerable<Part> secondaryPart, TeklaPluginRules pluginRules)
        {
            if (mainPart == null || secondaryPart == null)
                return;

            if (pluginRules.TypePlugin == TypePluginEnum.Component)
            {
                var component = new Component();
                component.Name = pluginRules.NameComponent;
                component.Number = pluginRules.NumberComponent;

                var componentInput = new ComponentInput();

                var arrayMain = new ArrayList();
                arrayMain.AddRange(mainPart.ToArray());

                var arraySecondary = new ArrayList();
                arraySecondary.AddRange(secondaryPart.ToArray());

                if (pluginRules.InputTypeMain == InputType.COLLECTION)
                {
                    componentInput.AddInputObjects(arrayMain);
                }
                else
                {
                    if (mainPart.Count() > 0)
                        componentInput.AddInputObject(mainPart.First());
                }

                if (pluginRules.InputTypeSecondary == InputType.COLLECTION)
                {
                    componentInput.AddInputObjects(arraySecondary);
                }
                else
                {
                    if (secondaryPart.Count() > 0)
                        componentInput.AddInputObject(secondaryPart.First());
                }

                component.SetComponentInput(componentInput);

                component.LoadAttributesFromFile(pluginRules.UserSetting);

                if (component.Insert())
                {
                    //component.LoadAttributesFromFile(pluginRules.UserSetting);
                    //component.Modify();

                    ConnectGuid = component.Identifier.GUID;
                }
            }

            if (pluginRules.TypePlugin == TypePluginEnum.Connection)
            {
                var connection = new Connection();
                connection.Name = pluginRules.NameComponent;
                connection.Number = pluginRules.NumberComponent;

                if(mainPart.Count()> 0)
                connection.SetPrimaryObject(mainPart.First());
                if (secondaryPart.Count() > 0)
                    connection.SetSecondaryObject(secondaryPart.First());

                connection.LoadAttributesFromFile(pluginRules.UserSetting);

                if (connection.Insert())
                {
                    //connection.LoadAttributesFromFile(pluginRules.UserSetting);
                    //connection.Modify();

                    ConnectGuid = connection.Identifier.GUID;
                }
            }
        }
    }
}
