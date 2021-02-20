using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Starter solution with the helper methods,
    //              opening and closing screens, and the menu
    // Application Type: Console
    // Author: Beckett, Michelle
    // Dated Created: 2/16/2021
    // Last Modified: 2/20/2021
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":

                        break;

                    case "d":

                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mix it up");
                Console.WriteLine("\td) Music notes");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayDance(finchRobot);
                        break;

                    case "c":
                        TalentShowDisplayMixItUp(finchRobot);
                        break;

                    case "d": TalentShowMusicNotes(finchRobot);

                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 75);
            }

            finchRobot.setLED(0, 255, 0);
            finchRobot.noteOn(784);
            finchRobot.wait(500);
            finchRobot.setLED(255, 0, 0);
            finchRobot.noteOn(523);
            finchRobot.wait(500);
            finchRobot.setLED(100, 0, 50);
            finchRobot.noteOn(684);
            finchRobot.wait(500);
            finchRobot.setLED(50, 100, 0);
            finchRobot.noteOn(988);
            finchRobot.wait(500);
            finchRobot.setLED(255, 0, 100);
            finchRobot.wait(500);
            finchRobot.noteOn(65);
            finchRobot.wait(500);
            finchRobot.setLED(150, 0, 255);
            finchRobot.wait(500);
            finchRobot.noteOn(880);
            finchRobot.wait(500);
            finchRobot.setLED(255, 150, 0);
            finchRobot.setLED(200, 0, 0);
            finchRobot.wait(500);
            finchRobot.setLED(100, 200, 0);
            finchRobot.wait(500);
            finchRobot.setLED(0, 100, 200);
            finchRobot.wait(500);
            finchRobot.noteOn(659);
            finchRobot.setLED(255, 200, 0);
            finchRobot.noteOn(0);
            finchRobot.setLED(0, 0, 0);


            DisplayMenuPrompt("Talent Show Menu");

            //****************************************************************************
            //          Talent show > Dance
            //****************************************************************************
        }
        static void TalentShowDisplayDance(Finch finchRobot)
        {
            Console.CursorVisible = false;

            Console.WriteLine("\tThe Finch will now dance for you!");

            DisplayContinuePrompt();

            finchRobot.setMotors(-30, 50);
            finchRobot.wait(1000);
            finchRobot.setMotors(100, 225);
            finchRobot.wait(1000);
            finchRobot.setMotors(-75, 130);
            finchRobot.wait(1000);
            finchRobot.setMotors(50, -10);
            finchRobot.wait(1000);
            finchRobot.setMotors(-30, 150);
            finchRobot.wait(1000);
            finchRobot.setMotors(-150, 50);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);
            finchRobot.setMotors(-30, 250);
            finchRobot.wait(1000);
            finchRobot.setMotors(100, 25);
            finchRobot.wait(1000);
            finchRobot.setMotors(-75, 30);
            finchRobot.wait(1000);
            finchRobot.setMotors(90, -10);
            finchRobot.wait(1000);
            finchRobot.setMotors(-30, 50);
            finchRobot.wait(1000);
            finchRobot.setMotors(-150, 50);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);


            DisplayMenuPrompt("Talent Show Menu");
        }

        //***********************************************************************
        //          Talent show > Mix it up
        //***********************************************************************

        static void TalentShowDisplayMixItUp(Finch finchRobot)

        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Mix it up");

            Console.WriteLine("\tThe Finch robot will not show off all of its moves!");

            DisplayContinuePrompt();

            finchRobot.noteOn(698);
            finchRobot.wait(1000);
            finchRobot.setLED(0, 255, 0);
            finchRobot.setMotors(100, 50);
            finchRobot.wait(1000);
            finchRobot.noteOn(784);
            finchRobot.setLED(255, 0, 0);
            finchRobot.setMotors(-250, 100);
            finchRobot.wait(1000);
            finchRobot.noteOn(698);
            finchRobot.setLED(0, 0, 255);
            finchRobot.setMotors(75, 150);
            finchRobot.wait(1000);
            finchRobot.noteOn(659);
            finchRobot.setLED(0, 255, 0);
            finchRobot.setMotors(40, -250);
            finchRobot.wait(1000);
            finchRobot.noteOn(587);
            finchRobot.setLED(255, 0, 0);
            finchRobot.setMotors(50, 160);
            finchRobot.wait(1000);
            finchRobot.noteOn(523);
            finchRobot.setLED(0, 0, 255);
            finchRobot.setMotors(-50, 150);
            finchRobot.wait(1000);
            finchRobot.noteOn(587);
            finchRobot.setLED(255, 0, 0);
            finchRobot.setMotors(0, 120);
            finchRobot.wait(1000);
            finchRobot.noteOn(659);
            finchRobot.setLED(0, 255, 0);
            finchRobot.setMotors(-250, 50);
            finchRobot.wait(1000);
            finchRobot.noteOn(684);
            finchRobot.setLED(0, 0, 255);
            finchRobot.setMotors(250, -120);
            finchRobot.wait(1000);
            finchRobot.noteOn(659);
            finchRobot.setLED(0, 255, 0);
            finchRobot.setMotors(0, 50);
            finchRobot.wait(1000);
            finchRobot.noteOn(587);
            finchRobot.setLED(0, 0, 255);
            finchRobot.setMotors(150, 70);
            finchRobot.wait(1000);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.setMotors(0, 0);

            DisplayMenuPrompt("Talent Show Menu");
        }

        //***********************************************************************
        //      Talent show > Music notes
        //***********************************************************************

        static void TalentShowMusicNotes(Finch finchRobot)
        {
            Console.CursorVisible = true;
            bool quitTalentShowMusicNotes = false;
            string musicLetter;

           
            { DisplayScreenHeader("Music notes");

                Console.WriteLine("\tPick a number for the finch to play");
                Console.WriteLine("\t1) note c");
                Console.WriteLine("\t2) note d");
                Console.WriteLine("\t3) note e");
                Console.WriteLine("\t4) note f");
                Console.WriteLine("\t5) note g");
                Console.WriteLine("\t6) note a");
                Console.WriteLine("\t7) note b");
                Console.Write("\t\tEnter choice");
                musicLetter = Console.ReadLine().ToLower();

                switch (musicLetter)
                {
                    case "1":
                        finchRobot.noteOn(523);
                        finchRobot.wait(1000);
                        finchRobot.noteOn(0);
                        break;

                    case "2":
                        finchRobot.noteOn(587);
                        finchRobot.wait(1000);
                        finchRobot.noteOn(0);
                        break;

                    case "3":
                        finchRobot.noteOn(659);
                        finchRobot.wait(1000);
                        finchRobot.noteOn(0);
                        break;

                    case "4":
                        finchRobot.noteOn(698);
                        finchRobot.wait(1000);
                        finchRobot.noteOn(0);
                        break;

                    case "5":
                        finchRobot.noteOn(784);
                        finchRobot.wait(1000);
                        finchRobot.noteOn(0);
                        break;

                    case "6":
                        finchRobot.noteOn(880);
                        finchRobot.wait(1000);
                        finchRobot.noteOn(0);
                        break;

                    case "7":
                        finchRobot.noteOn(988);
                        finchRobot.wait(1000);
                        finchRobot.noteOn(0);
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                        
                }DisplayMenuPrompt("Talent Show Menu");
               } 
           } 
        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}



