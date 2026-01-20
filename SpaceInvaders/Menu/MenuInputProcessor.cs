//using SpaceInvaders.Game;
//using SpaceInvaders.Menu.Common;
//using SpaceInvaders.Menu.Enums;
//using SpaceInvaders.System;

//namespace SpaceInvaders.Menu
//{
//    public static class MenuInputProcessor
//    {
//#region MainMenu

//        /// <summary>
//        /// Processes all pending input commands and updates the main menu state accordingly.
//        /// </summary>
//        /// <remarks>This method handles main menu navigation</remarks>
//        public static MenuState ProcessMainMenuInput(InputCommand input, MenuState state)
//        {
//                switch (input.Type)
//                {
//                    case SystemInputCommandType.UP:
//                    state.CurrentOption = state.CurrentOption with { position = Math.Max((int)MainMenuOption.StartGame, state.CurrentOption.position - 1) }; //restrict StartGame as first option
//                         break;
//                    case SystemInputCommandType.DOWN:
//                    state.CurrentOption = state.CurrentOption with { position = Math.Min(MenuOptionsProvider.MainMenuOptionsCount - 1, state.CurrentOption.position + 1) };
//                         break;
//                    case SystemInputCommandType.ENTER:
//                    state = ProcessMainMenuEnterClick(state);
//                         break;
//                    case SystemInputCommandType.ESCAPE:
//                    throw new OperationCanceledException();
//            }
//            return state;
//        }

//        /// <summary>
//        /// Processes all pending input commands and updates the main menu state accordingly.
//        /// </summary>
//        /// <remarks>This method handles main menu navigation</remarks>
//        private static MenuState ProcessMainMenuEnterClick( MenuState state)
//        {
//            state.EnterClicked = false; // Reset Enter Clicked after processing

//            state.ScreenState = (MainMenuOption)state.CurrentOption.position switch
//            {
//                MainMenuOption.StartGame => Common.ScreenState.Gameplay,
//                MainMenuOption.GameRecords => Common.ScreenState.GameRecords,
//                MainMenuOption.Settings => Common.ScreenState.SettingsMenu,
//                MainMenuOption.Credits => Common.ScreenState.Credits,
//                MainMenuOption.Exit => state.ScreenState, // or handle exit elsewhere
//                _ => state.ScreenState
//            };

//            return state;
//        }
// #endregion

//        /// <summary>
//        /// Processes all pending input commands and updates the main menu state accordingly.
//        /// </summary>
//        /// <remarks>This method handles main menu navigation</remarks>
//        public static MenuState ProcessPauseMenuInput(InputCommand input, MenuState state)
//        {
//            switch (input.Type)
//            {
//                case SystemInputCommandType.UP:
//                    state.CurrentOption = state.CurrentOption with { position = Math.Max(0, state.CurrentOption.position - 1) };
//                    break;
//                case SystemInputCommandType.DOWN:
//                    state.CurrentOption = state.CurrentOption with { position = Math.Min(MenuOptionsProvider.PauseMenuLongestOptionLength, state.CurrentOption.position + 1) };
//                    break;
//                case SystemInputCommandType.ENTER:
//                    state = ProcessPauseMenuEnterClick(state);
//                    break;
//                case SystemInputCommandType.ESCAPE:
//                    state = ProcessPauseMenuEscapeClick(state);
//                    break;
//            }
//            return state;
//        }

//        /// <summary>
//        /// Processes all pending input commands and updates the main menu state accordingly.
//        /// </summary>
//        /// <remarks>This method handles main menu navigation</remarks>
//        private static MenuState ProcessPauseMenuEnterClick(MenuState state)
//        {
//            state.EnterClicked = false; // Reset Enter Clicked after processing

//            state.ScreenState = (PauseMenuOption)state.CurrentOption.position switch
//            {
//                PauseMenuOption.Resume => Common.ScreenState.Gameplay,
//                PauseMenuOption.Restart => Common.ScreenState.Gameplay,
//                PauseMenuOption.Exit => Common.ScreenState.MainMenu,
//                _ => state.ScreenState
//            };

//            return state;
//        }

//        /// <summary>
//        /// Processes all pending input commands and updates the main menu state accordingly.
//        /// </summary>
//        /// <remarks>This method handles main menu navigation</remarks>
//        private static MenuState ProcessPauseMenuEscapeClick(MenuState state)
//        {
//            state.EscapeClicked = false; // Reset Enter Clicked after processing

//            state.ScreenState = Common.ScreenState.Gameplay;
//            return state;
//        }

//        /// <summary>
//        /// Processes all pending input commands and updates the main menu state accordingly.
//        /// </summary>
//        /// <remarks>This method handles main menu navigation</remarks>
//        public static MenuState ProcessGameOverMenuInput(InputCommand input, MenuState state)
//        {
//            switch (input.Type)
//            {
//                case SystemInputCommandType.UP:
//                    state.CurrentOption = state.CurrentOption with { position = Math.Max(0, state.CurrentOption.position - 1) };
//                    break;
//                case SystemInputCommandType.DOWN:
//                    state.CurrentOption = state.CurrentOption with { position = Math.Min(MenuOptionsProvider.GameOverMenuLongestOptionLength, state.CurrentOption.position + 1) };
//                    break;
//                case SystemInputCommandType.ENTER:
//                    //Enter Logic Here
//                    break;
//            }
//            return state;
//        }
//    }
//}
