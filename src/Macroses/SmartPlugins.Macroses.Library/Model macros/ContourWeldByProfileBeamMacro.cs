using Autofac;
using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.Exceptions;
using SmartPlugins.Common.TeklaLibrary.Welds;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace SmartPlugins.Macroses.Library
{
    public class ContourWeldByProfileBeamMacro : ITeklaMacro
    {
        /// <inheritdoc/>
        public void RunLoop() => ErrorCatcher.Try(() => { while (true) { Macro(); } });

        /// <inheritdoc/>
        public void RunOnce() => ErrorCatcher.Try(() => { Macro(); });

        /// <summary>
        /// Logic of a macro
        /// </summary>
        private void Macro()
        {
            var container = MacrosContainerConfigure.GetContainer().Build();

            var smartModel = container.Resolve<ISmartModel>();
            var picker = container.Resolve<ISmartPicker>();
            var mainPart = picker.PickObject<Part>((int)Picker.PickObjectEnum.PICK_ONE_PART);
            var secondaryPart = picker.PickObject<Beam>((int)Picker.PickObjectEnum.PICK_ONE_PART);

            if (mainPart == null || secondaryPart == null)
                throw new UserInputException(MessagesEN.MacroUserInputExeption);

            new ContourWeldByProfileBeam(smartModel).Get(mainPart, secondaryPart);

        }
    }
}
