using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using Microsoft.MixedReality.Toolkit;

namespace Source.UserInput
{
    public class KeyboardController : BaseController
    {
        public KeyboardController(TrackingState trackingState, Handedness controllerHandedness,
            IMixedRealityInputSource inputSource = null, MixedRealityInteractionMapping[] interactions = null) : base(
            trackingState, controllerHandedness, inputSource, interactions)
        { }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //IMixedRealityPointerHandler.OnPointerDragged(InputSource, Handedness.None, DefaultInteractions[0].MixedRealityInputAction);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                MixedRealityToolkit.InputSystem?.RaiseOnInputUp(InputSource, Handedness.None, DefaultInteractions[0].MixedRealityInputAction);
            }
        }

        public override MixedRealityInteractionMapping[] DefaultInteractions { get; } = {
            new MixedRealityInteractionMapping(1, "Keyboard Space Bar", AxisType.Digital, DeviceInputType.Select,
                new MixedRealityInputAction(1, "Select", AxisType.Digital), KeyCode.Space)
        };

        public override void SetupDefaultInteractions(Handedness controllerHandedness)
        {
            AssignControllerMappings(DefaultInteractions);
        }
    }
}