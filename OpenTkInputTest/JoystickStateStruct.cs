namespace OpenTkInputTest
{
    using OpenTK.Windowing.Common.Input;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    public struct JoystickStateStruct
    {
        public readonly int JoystickId;
        public readonly float[] AxisValues;
        public readonly bool[] ButtonStates;
        public readonly Hat[] HatPositions;

        public JoystickStateStruct(JoystickState state)
        {
            JoystickId = state.Id;
            AxisValues = new float[state.AxisCount];
            ButtonStates = new bool[state.ButtonCount];
            HatPositions = new Hat[state.HatCount];

            Update(state);
        }

        public void Update(JoystickState state)
        {
            for (int axisId = 0; axisId < AxisValues.Length; axisId++)
            {
                AxisValues[axisId] = state.GetAxis(axisId);
            }
            for (int buttonId = 0; buttonId < ButtonStates.Length; buttonId++)
            {
                ButtonStates[buttonId] = state.IsButtonDown(buttonId);
            }
            for (int hatId = 0; hatId < HatPositions.Length; hatId++)
            {
                HatPositions[hatId] = state.GetHat(hatId);
            }
        }
    }
}
