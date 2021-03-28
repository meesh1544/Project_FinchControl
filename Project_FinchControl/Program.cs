using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FinchAPI;

namespace Project_FinchControl
{
    /// <summary>
    /// user commands
    /// </summary>
    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        NOTEON,
        GETTEMPERATURE,
        DANCE,
        LIGHTSHOW,
        DONE
    }

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
        private int numberOfDataPoints;

        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DisplaySetTheme();

            DisplayWelcomeScreen();
            DisplayLoginRegister();

            DisplayMenuScreen();
            DisplayClosingScreen();


        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) themeColors;
            Console.CursorVisible = true;
            themeColors = ReadThemeData();
            Console.ForegroundColor = themeColors.foregroundColor;
            Console.BackgroundColor = themeColors.backgroundColor;

            //Console.ForegroundColor = ConsoleColor.DarkBlue;
            //Console.BackgroundColor = ConsoleColor.White;
        }


        /// <summary>       
        /// setup new console theme color
        /// </summery>
        static void DisplaySetTheme()
        {
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) themeColors;
            bool themeChosen = false;


            //set current theme from data

            themeColors = ReadThemeData();
            Console.ForegroundColor = themeColors.foregroundColor;
            Console.BackgroundColor = themeColors.backgroundColor;
            Console.Clear();
            DisplayScreenHeader("Set Application Theme");

            Console.WriteLine($"\tCurrent foreground color: {Console.ForegroundColor}");
            Console.WriteLine($"\tCurrent background color: {Console.BackgroundColor}");
            Console.WriteLine();

            Console.Write("\tWould you like to change the current theme [ yes | no ]?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                do
                {
                    themeColors.foregroundColor = getColsoleColorFromUser("foreground");
                    themeColors.backgroundColor = getColsoleColorFromUser("background");

                    ///
                    ///set new theme
                    ///
                    Console.ForegroundColor = themeColors.foregroundColor;
                    Console.BackgroundColor = themeColors.backgroundColor;
                    Console.Clear();
                    DisplayScreenHeader("Set Application Theme");
                    Console.WriteLine($"\tNew foreground color: {Console.ForegroundColor}");
                    Console.WriteLine($"\tNew background color: {Console.BackgroundColor}");

                    Console.WriteLine();
                    Console.Write("\tIs this the theme you would like?");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        themeChosen = true;
                        WriteThemeData(themeColors.foregroundColor, themeColors.backgroundColor);
                    }

                } while (!themeChosen);
            }
            DisplayContinuePrompt();
        }

        static void DisplayNewThemeColor()
        {
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) themeColor;
            bool newTheme = false;
            string fileIOStatusMessage;

            DisplayScreenHeader("Set New Theme");

            themeColor = ReadThemeData();
            Console.WriteLine($"\tCurent foreground color: {Console.ForegroundColor}");
            Console.WriteLine($"\tCurrent background color: {Console.BackgroundColor}");
            Console.WriteLine();

            Console.Write("\tWould you like to change the current theme [yes | no]?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                do
                {
                    ///
                    ///ask the user for console colors
                    ///
                    themeColor.foregroundColor = getColsoleColorFromUser("foreground");
                    themeColor.backgroundColor = getColsoleColorFromUser("background");
                    Console.Clear();

                    DisplayScreenHeader("Set the Application Theme Color");
                    Console.WriteLine($"\tNew foreground color: {Console.ForegroundColor}");
                    Console.WriteLine($"\tNew background color: {Console.BackgroundColor}");

                    Console.WriteLine();
                    Console.WriteLine("\tIs this the color you would like for the Theme?");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        newTheme = true;
                        fileIOStatusMessage = WriteThemeDataException(themeColor.foregroundColor, themeColor.backgroundColor);
                        if (fileIOStatusMessage == "Complete")
                        {
                            Console.WriteLine("\n\tNew theme written to data file.\n");
                        }
                        else
                        {
                            Console.WriteLine("\tThe new theme is not in the data file");
                            Console.WriteLine($"\t*** {fileIOStatusMessage} ***\t");
                        }
                    }
                } while (!newTheme);
            }
            DisplayContinuePrompt();
        }

        static ConsoleColor getColsoleColorFromUser(string property)
        {
            ConsoleColor consoleColor;
            bool validConsoleColor;

            do
            {
                Console.Write($"\tEnter a value for the {property}:");
                validConsoleColor = Enum.TryParse<ConsoleColor>(Console.ReadLine(), true, out consoleColor);

                if (!validConsoleColor)
                {
                    Console.WriteLine("\n\t***** It appears you did not provide a valid console color. Please try again. *****\n");
                }
                else
                {
                    validConsoleColor = true;
                }

            } while (!validConsoleColor);

            return consoleColor;
        }

        #region USER INTERFACE




        ///<summary>
        ///Data display
        /// </summary>

        ///<summery>
        ///read theme data from files
        /// </summery>

        static (ConsoleColor foregroundColor, ConsoleColor backgroundColor) ReadThemeData()
        {
            string dataPath = @"Data/Theme.txt";
            string[] themeColors;

            ConsoleColor foregroundColor;
            ConsoleColor backgroundColor;

            themeColors = File.ReadAllLines(dataPath);

            Enum.TryParse(themeColors[0], true, out foregroundColor);
            Enum.TryParse(themeColors[1], true, out backgroundColor);

            return (foregroundColor, backgroundColor);
        }

        ///<summery>
        ///write data file
        /// </summery>

        static void WriteThemeData(ConsoleColor foreground, ConsoleColor background)
        {
            string dataPath = @"Data/Theme.txt";

            File.WriteAllText(dataPath, foreground.ToString() + "\n");
            File.AppendAllText(dataPath, background.ToString());


        }

        static string WriteThemeDataException(ConsoleColor foreground, ConsoleColor background)
        {
            string dataPath = @"data/theme.txt";
            string fileIOStatusMessage = "";

            try
            {
                File.WriteAllText(dataPath, foreground.ToString() + "\n");
                File.AppendAllText(dataPath, background.ToString());
                fileIOStatusMessage = "Complete";
            }
            catch (DirectoryNotFoundException)
            {
                fileIOStatusMessage = "Unable to locate the folder for the data file.";
            }
            catch (Exception)
            {
                fileIOStatusMessage = "Unable to write to data file.";
            }

            return fileIOStatusMessage;
        }

        ///<summery>
        ///read theme exceptions from data file
        ///returns an IO message
        /// </summery>
        ///<returns> a tuple of foreground and background</returns>

        static (ConsoleColor forgroundColor, ConsoleColor backgroundColor) ReadThemeDataExpections(out string fileIOStatusMessage)
        {
            string dataPath = @"Data/Theme.txt";
            string[] themeColors;

            ConsoleColor foregroundColor = ConsoleColor.White;
            ConsoleColor backgroundColor = ConsoleColor.Black;

            try
            {
                themeColors = File.ReadAllLines(dataPath);
                if (Enum.TryParse(themeColors[0], true, out foregroundColor) &&
                    Enum.TryParse(themeColors[1], true, out backgroundColor))
                {
                    fileIOStatusMessage = "Complete";
                }
                else
                {
                    fileIOStatusMessage = "Data file incorrectly formated.";
                }
            }
            catch (DirectoryNotFoundException)
            {
                fileIOStatusMessage = "Unable to locate the folder for the data file.";
            }
            catch (Exception)
            {
                fileIOStatusMessage = "Unable to read data file.";
            }

            return (foregroundColor, backgroundColor);
        }




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
                Console.WriteLine("\tf) Theme Change");
                Console.WriteLine("\tg) Disconnect Finch Robot");
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
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        LightAlarmDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        UserProgrammingDisplayProgrammingScreen(finchRobot);
                        break;

                    case "f":
                        DataDisplaySetTheme(finchRobot);
                        break;

                    case "g":
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

        /// <summary>
        /// *****************************************************************
        /// *                 Login/Register Screen                         *
        /// *****************************************************************
        /// </summary>
        static void DisplayLoginRegister()
        {
            DisplayScreenHeader("Login/Register");

            Console.Write("\tAre you a registered user [ yes | no ]?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                DisplayLogin();
            }
            else
            {
                DisplayRegisterUser();
                DisplayLogin();
            }
        }

        /// <summary>
        /// *****************************************************************
        /// *                          Login Screen                         *
        /// *****************************************************************
        /// </summary>
        static void DisplayLogin()
        {
            string userName;
            string password;
            bool validLogin;

            do
            {
                DisplayScreenHeader("Login");

                Console.WriteLine();
                Console.Write("\tEnter your user name:");
                userName = Console.ReadLine();
                Console.Write("\tEnter your password:");
                password = Console.ReadLine();

                validLogin = IsValidLoginInfo(userName, password);

                Console.WriteLine();
                if (validLogin)
                {
                    Console.WriteLine("\tYou are now logged in.");
                }
                else
                {
                    Console.WriteLine("\tIt appears either the user name or password is incorrect.");
                    Console.WriteLine("\tPlease try again.");
                }

                DisplayContinuePrompt();
            } while (!validLogin);
        }

        /// <summary>
        /// check user login
        /// </summary>
        /// <param name="userName">user name entered</param>
        /// <param name="password">password entered</param>
        /// <returns>true if valid user</returns>
        static bool IsValidLoginInfo(string userName, string password)
        {
            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();
            bool validUser = false;

            registeredUserLoginInfo = ReadLoginInfoData();

            //
            // loop through the list of registered user login tuples and check each one against the login info
            //
            foreach ((string userName, string password) userLoginInfo in registeredUserLoginInfo)
            {
                if ((userLoginInfo.userName == userName) && (userLoginInfo.password == password))
                {
                    validUser = true;
                    break;
                }
            }

            return validUser;
        }

        /// <summary>
        /// *****************************************************************
        /// *                       Register Screen                         *
        /// *****************************************************************
        /// write login info to data file
        /// </summary>
        static void DisplayRegisterUser()
        {
            string userName;
            string password;

            DisplayScreenHeader("Register");

            Console.Write("\tEnter your user name:");
            userName = Console.ReadLine();
            Console.Write("\tEnter your password:");
            password = Console.ReadLine();

            WriteLoginInfoData(userName, password);

            Console.WriteLine();
            Console.WriteLine("\tYou entered the following information and it has be saved.");
            Console.WriteLine($"\tUser name: {userName}");
            Console.WriteLine($"\tPassword: {password}");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// read login info from data file
        /// Note: no error or validation checking
        /// </summary>
        /// <returns>list of tuple of user name and password</returns>
        static List<(string userName, string password)> ReadLoginInfoData()
        {
            string dataPath = @"Data/Logins.txt";

            string[] loginInfoArray;
            (string userName, string password) loginInfoTuple;

            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();

            loginInfoArray = File.ReadAllLines(dataPath);

            //
            // loop through the array
            // split the user name and password into a tuple
            // add the tuple to the list
            //
            foreach (string loginInfoText in loginInfoArray)
            {
                //
                // use the Split method to separate the user name and password into an array
                //
                loginInfoArray = loginInfoText.Split(',');

                loginInfoTuple.userName = loginInfoArray[0];
                loginInfoTuple.password = loginInfoArray[1];

                registeredUserLoginInfo.Add(loginInfoTuple);

            }

            return registeredUserLoginInfo;
        }

        /// <summary>
        /// write login info to data file
        /// Note: no error or validation checking
        /// </summary>
        static void WriteLoginInfoData(string userName, string password)
        {
            string dataPath = @"Data/Logins.txt";
            string loginInfoText;

            loginInfoText = userName + "," + password;

            //
            // use the AppendAllText method to not overwrite the existing logins
            //
            File.AppendAllText(dataPath, loginInfoText);
        }

        #region USER INTERFACE


        private static void DataDisplaySetTheme(Finch finchRobot)
        {
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) themeColors;
            bool themeChosen = false;
            ///
            ///set theme from data
            ///
            themeColors = ReadThemeData();
            Console.ForegroundColor = themeColors.foregroundColor;
            Console.BackgroundColor = themeColors.backgroundColor;
            Console.Clear();

            DisplayScreenHeader("Set Application Theme");

            Console.WriteLine($"\tCurrent foreground color: {Console.ForegroundColor}");
            Console.WriteLine($"\tCurrent background color: {Console.BackgroundColor}");
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
            //bool quitTalentShowMusicNotes = false;
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

                } DisplayMenuPrompt("Talent Show Menu");
            }
        }
        #endregion

        #region Data Recorder

        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] celsius = null;

            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\te) Read From Data File");
                Console.WriteLine("\tf) Write From Data File");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        celsius = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        DataRecorderDisplayData(celsius);
                        break;

                    case "e":
                        ((int)numberOfDataPoints, dataPointFrequency) DataRecorderReadFromDataFile();
                        break;

                    case "f":
                        DataRecorderWriteToDataFile();
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

        static void (int numberOfDataPoints, dataPointFrequency) DataRecorderWriteToDataFile()
        {
            string dataPath = @"Data/Temp.txt";
            string[] temps;
            double[] dataPointFrequency;
            double[] celsius = new double[numberOfDataPoints];
            temps = File.ReadAllLines(dataPath);

            Enum.TryParse(temps[0], true, out numberOfDataPoints);
            Enum.TryParse(temps[1], true, out dataPointFrequency);

            return (int numberOfDataPoints, dataPointFrequency);
        }
    
        static void DataRecorderReadFromDataFile()
        {
            string dataPath = @"Data/Temp.txt";

            File.WriteAllText(dataPath, numberOFDataPoints.ToString() + "\n");
            File.AppendAllText(dataPath, dataPointFrequency.ToString());
        }

        static void DataRecorderDisplayData(double[] celsius)
        {
            DisplayScreenHeader("Show data");

            DataRecorderDisplayTable(celsius);

            DisplayContinuePrompt();
        }

        static void DataRecorderDisplayTable(double[] celsius)
        {
            //
            //display table header
            //
            Console.WriteLine(
             "-----------".PadLeft(15) +
             "-----------".PadLeft(14)
             );

            Console.WriteLine(
                "Recording #".PadLeft(14) +
                "Temp in F*".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(14)
                );

            //
            //display data table
            //
            for (int index = 0; index < celsius.Length; index++)
            {
                Console.WriteLine(
                   (index + 1).ToString().PadLeft(14) +
                   (celsius[index] * 9 / 5 + 32).ToString("n2").PadLeft(14)
                     );
            }
        }




        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            //double[] temperatures = new double[numberOfDataPoints];
            double[] celsius = new double[numberOfDataPoints];
            DisplayScreenHeader("Get Data");

            Console.WriteLine($"\tNumber of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"\tData Point Frequency: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine("\tThe finch robot is now ready to begin temperature recording");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                //temperatures[index] = finchRobot.getTemperature();
                //Console.WriteLine();
                //Console.WriteLine($"\tReading {index + 1}: {temperatures[index].ToString("n2")}");
                //int waitInSeconds = (int)(dataPointFrequency * 1000);
                //finchRobot.wait(1000);

                celsius[index] = finchRobot.getTemperature();
                double farenheight = (celsius[index] * 9 / 5 + 32);
                Console.WriteLine($"\t{index + 1}C*: {celsius[index]} F*: {farenheight} ");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(1000);
            }

            DisplayContinuePrompt();
            DisplayScreenHeader("Get Data");

            Console.WriteLine();
            Console.WriteLine("\tTable of temperatures");
            Console.WriteLine();
            DataRecorderDisplayTable(celsius);

            return celsius;
        }

        /// <summary>
        /// get the frequency of data points from the user
        /// </summary>
        /// <returns>get the data points</returns>
        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double DataPointFrequency;

            DisplayScreenHeader("Data Point Frequency");

            Console.Write("\tFrequency of data points: ");

            // validate user input
            double.TryParse(Console.ReadLine(), out DataPointFrequency);

            DisplayContinuePrompt();

            return DataPointFrequency;
        }

        /// <summary>
        /// get the number of data points from the user
        /// </summary>
        /// <returns>number of data points</returns>
        static int DataRecorderDisplayGetDataPoints()
        {
            int numberOfDataPoints;
            string userResonse;

            DisplayScreenHeader("Number of Data Points");

            Console.Write("\tNumber of data points: ");
            userResonse = Console.ReadLine();

            // validate user input
            int.TryParse(userResonse, out numberOfDataPoints);

            DisplayContinuePrompt();

            return numberOfDataPoints;
        }



        #endregion

        /// <summary>
        /// *****************************************************************
        /// *               Light Alarm Menu                  *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>

        #region Alarm System


        static void LightAlarmDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            string sensorsToMonitorInput = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;
            do
            {
                DisplayScreenHeader("Light Alarm Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitorInput = LightAlarmDisplaySetSensorToMonitorInput();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplayRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmSetMinMaxThresholdValue(rangeType, finchRobot);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmSetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmSetAlarm(finchRobot, sensorsToMonitorInput, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

        static void LightAlarmSetAlarm(
            Finch finchRobot,
            string sensorsToMonitorInput,
            string rangeType,
            int minMaxThresholdValue,
            int timeToMonitor)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;

            string sensorsToMonitor = null;
            string rangeTypeOutput = null;

            if (sensorsToMonitorInput == "a")
            {
                sensorsToMonitor = "Left";
            }

            if (sensorsToMonitorInput == "b")
            {
                sensorsToMonitor = "Right";
            }

            if (sensorsToMonitorInput == "c")
            {
                sensorsToMonitor = "Both";
            }

            if (rangeType == "a")
            {
                rangeTypeOutput = "Minimum";
            }

            if (rangeType == "b")
            {
                rangeTypeOutput = "Maximum";
            }


            DisplayScreenHeader("Set Alarm");

            Console.WriteLine("\tSensor to Monitor: {0}", sensorsToMonitor);
            Console.WriteLine("\tRange Type: {0}", rangeTypeOutput);
            Console.WriteLine("\t{0} Min/Max Threshold Value: {1}", rangeTypeOutput, minMaxThresholdValue);
            Console.WriteLine("\tTime to monitor: {0}", timeToMonitor);
            Console.WriteLine();

            Console.WriteLine("\tPress any key to begin monitoring");
            Console.ReadKey();


            while (secondsElapsed < timeToMonitor && !thresholdExceeded)
            {
                switch (sensorsToMonitorInput)
                {
                    case "a":
                        currentLightSensorValue = finchRobot.getLeftLightSensor();
                        break;

                    case "b":
                        currentLightSensorValue = finchRobot.getRightLightSensor();
                        break;

                    case "c":
                        currentLightSensorValue = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
                        break;

                }

                switch (rangeType)
                {
                    case "a":
                        if (currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "b":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                finchRobot.wait(1000);
                secondsElapsed++;
                Console.WriteLine("\tThe current value: {0}", currentLightSensorValue);
            }

            if (thresholdExceeded)
            {
                Console.WriteLine($"\tThe {rangeTypeOutput} threshold value was exceeded.");
                finchRobot.setLED(0, 0, 255);
                finchRobot.noteOn(397);
                finchRobot.wait(1000);
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOn(0);
            }

            else
            {
                Console.WriteLine($"\tThe {rangeTypeOutput} threshold value  was not exceeded.");
                finchRobot.setLED(255, 0, 0);
                finchRobot.noteOn(397);
                finchRobot.wait(1000);
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOn(0);
            }

            DisplayMenuPrompt("Light Alarm");
        }

        static int LightAlarmSetTimeToMonitor()
        {
            int timeToMonitor;
            bool currentTimeToMonitor;
            DisplayScreenHeader("\tSet time to Monitor");

            //validate value
            Console.Write("\tHow long would you like to monitor in seconds?");
            currentTimeToMonitor = int.TryParse(Console.ReadLine(), out timeToMonitor);

            //echo value
            if (!currentTimeToMonitor)
            {
                DisplayScreenHeader("Please enter an amount in seconds");
                DisplayContinuePrompt();
                return LightAlarmSetTimeToMonitor();
            }
            else
            {
                return timeToMonitor;
            }


        }


        static int LightAlarmSetMinMaxThresholdValue(string rangeType, Finch finchRobot)
        {
            int minMaxThresholdValue;
            string rangeTypeOutput = null;
            bool minMaxThresholdValueCurrent;
            if (rangeType == "a")
            {
                rangeTypeOutput = "Minimum";
            }
            if (rangeType == "b")
            {
                rangeTypeOutput = "Maximum";
            }
            DisplayScreenHeader("\tMinimum/Maximum Threshold Value");

            Console.WriteLine($"\tLeft light sensor ambient value: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine($"\tRight light sensor ambient value: {finchRobot.getRightLightSensor()}");
            Console.WriteLine();

            //validate value
            Console.Write($"\tEnter the {rangeTypeOutput} light value");
            minMaxThresholdValueCurrent = int.TryParse(Console.ReadLine(), out minMaxThresholdValue);

            //echo value

            if (minMaxThresholdValueCurrent)
            {
                DisplayScreenHeader("Please enter an integer");
                DisplayContinuePrompt();
                return minMaxThresholdValue;
            }

            DisplayMenuPrompt("Light Alarm");

            return minMaxThresholdValue;
        }

        static string LightAlarmDisplaySetSensorToMonitorInput()
        {
            string sensorsToMonitorInput;
            string[] sensorOption = new string[] { "a", "b", "c" };

            DisplayScreenHeader("Sensors to Monitor");

            Console.Write("\ta) Left sensor to monitor");
            Console.WriteLine("\tb) Right sensor to monitor");
            Console.WriteLine("\tc) Both sensors to monitor");

            Console.Write("\tWhich sensor would you like to monitor?");
            sensorsToMonitorInput = Console.ReadLine().ToLower();

            if (sensorOption.Contains(sensorsToMonitorInput))
            {
                return sensorsToMonitorInput;
            }
            else
            {
                DisplayScreenHeader("Please choose a letter.");
                DisplayContinuePrompt();
                return LightAlarmDisplaySetSensorToMonitorInput();

            }

        }

        static string LightAlarmDisplayRangeType()
        {
            string rangeType;
            string[] minOrMaxRange = new string[] { "a", "b" };
            DisplayScreenHeader("Range Type");

            Console.Write("\tSet range Type Minimum or Maximum");

            Console.WriteLine("\ta) Minimum");
            Console.WriteLine("\tb) Maximum");
            rangeType = Console.ReadLine().ToLower();

            if (minOrMaxRange.Contains(rangeType))
            {
                return rangeType;
            }
            else
            {
                DisplayScreenHeader("Please choose minimum or maximum");
                DisplayContinuePrompt();
                return LightAlarmDisplayRangeType();
            }


        }
        #endregion

        /// <summary>
        /// *********************************************************************
        ///                     *     User Programming      *
        /// *********************************************************************

        private static void UserProgrammingDisplayProgrammingScreen(Finch finchRobot)

        {
            string menuChoice;
            bool quitMenu = false;

            //
            // Tuple
            //
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParamaters;
            commandParamaters.motorSpeed = 0;
            commandParamaters.ledBrightness = 0;
            commandParamaters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("User Programming Menu");
                //
                //get user menu choice
                //
                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\te) Load User Program");
                Console.WriteLine("\tf) Read User Program");
                Console.WriteLine("\tq) Quit");
                Console.WriteLine("\tEnter Choice");
                menuChoice = Console.ReadLine().ToLower();

                //
                //process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParamaters = UserProgrammingDisplayGetCommandParamaters();
                        break;

                    case "b":
                        UserProgrammingDisplayGetFinchCommands(commands);
                        break;

                    case "c":
                        UserProgrammingDisplayFinchCommands(commands);
                        break;

                    case "d":
                        UserProgrammingDisplayExecuteFinchCommands(finchRobot, commands, commandParamaters);
                        break;

                    case "e":
                        LoadUserProgram();
                        break;

                    case "f":
                        WriteUserProgram();
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter from the menu choice");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);

        }

        /// <summary>
        /// execute commands
        /// </summary>

        static void UserProgrammingDisplayExecuteFinchCommands
            (Finch finchRobot, List<Command> commands,
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParamaters)
        {
            int motorSpeed = commandParamaters.motorSpeed;
            int ledBrightness = commandParamaters.ledBrightness;
            int waitMilliSeconds = (int)(commandParamaters.waitSeconds * 1000);
            string commandFeedback = "";
            const int Turning_Motor_Speed = 100;

            DisplayScreenHeader("Execute Finch Commands");

            Console.WriteLine("\tThe Finch robot is ready to execute the list of demands");
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.MOVEFORWARD.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        commandFeedback = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        commandFeedback = Command.STOPMOTORS.ToString();
                        break;

                    case Command.DANCE:
                        finchRobot.setMotors(Turning_Motor_Speed, -Turning_Motor_Speed);
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        finchRobot.wait(waitMilliSeconds);
                        finchRobot.setMotors(0, 0);
                        finchRobot.setLED(0, 0, 0);
                        commandFeedback = Command.TURNLEFT.ToString();
                        commandFeedback = Command.TURNRIGHT.ToString();
                        commandFeedback = Command.WAIT.ToString();
                        commandFeedback = Command.LEDON.ToString();
                        commandFeedback = Command.DANCE.ToString();
                        break;

                    case Command.NOTEON:
                        finchRobot.noteOn(698);
                        commandFeedback = Command.NOTEON.ToString();
                        break;

                    case Command.LIGHTSHOW:
                        finchRobot.noteOn(698);
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        finchRobot.wait(waitMilliSeconds);
                        finchRobot.noteOn(0);
                        finchRobot.setLED(0, 0, 0);
                        commandFeedback = Command.NOTEON.ToString();
                        commandFeedback = Command.LEDON.ToString();
                        commandFeedback = Command.WAIT.ToString();
                        commandFeedback = Command.LIGHTSHOW.ToString();
                        break;

                    case Command.WAIT:
                        finchRobot.wait(waitMilliSeconds);
                        commandFeedback = Command.WAIT.ToString();
                        break;

                    case Command.TURNRIGHT:
                        finchRobot.setMotors(Turning_Motor_Speed, -Turning_Motor_Speed);
                        commandFeedback = Command.TURNRIGHT.ToString();
                        break;

                    case Command.TURNLEFT:
                        finchRobot.setMotors(-Turning_Motor_Speed, Turning_Motor_Speed);
                        commandFeedback = Command.TURNLEFT.ToString();
                        break;

                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandFeedback = Command.LEDON.ToString();
                        break;

                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        commandFeedback = Command.LEDOFF.ToString();
                        break;

                    case Command.GETTEMPERATURE:
                        commandFeedback = $"Temperature {finchRobot.getTemperature().ToString("n2")}\n";
                        break;

                    case Command.DONE:
                        commandFeedback = Command.DONE.ToString();
                        break;

                    default:

                        break;
                }
                Console.WriteLine($"\t{commandFeedback}");
            }
            DisplayMenuPrompt("User Programming");


        }

        static void UserProgrammingDisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("\tFinch Robot Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine($"\t{command}");
            }
            DisplayMenuPrompt("User Programming");
        }

        static void UserProgrammingDisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayScreenHeader("Finch Robot Commands");

            //
            //list commands
            //
            int commandCount = 1;
            Console.WriteLine("\tList of available commands");
            Console.WriteLine();
            Console.WriteLine("\t-");
            foreach (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"-, {commandName.ToLower()} -");
                if (commandCount % 5 == 0) Console.Write("-\n\t-");
                commandCount++;
            }

            Console.WriteLine();
            while (command != Command.DONE)
            {
                Console.Write("\tEnter Command: ");
                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("\t******************************************");
                    Console.WriteLine("\tPlease enter a command from the list above");
                    Console.WriteLine("\t*******************************************");
                }
            }
            DisplayMenuPrompt("User Programming");
        }
        /// <summary>
        /// get command paramaters from user
        /// </summary>
        static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandParamaters()
        {

            DisplayScreenHeader("Command Parameters");

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParamaters;
            commandParamaters.motorSpeed = 0;
            commandParamaters.ledBrightness = 0;
            commandParamaters.waitSeconds = 0;


            GetValidInteger("\tEnter motor speed [1- 255]: ", 1, 255, out commandParamaters.motorSpeed);
            GetValidInteger("\tEnter LED brightness [1- 255]: ", 1, 255, out commandParamaters.ledBrightness);
            GetValidDouble("\tEnter wait in seconds: ", 0, 10, out commandParamaters.waitSeconds);

            Console.WriteLine();
            Console.WriteLine($"\tMotor speed: {commandParamaters.motorSpeed}");
            Console.WriteLine($"\tLED brightness: {commandParamaters.ledBrightness}");
            Console.WriteLine($"\tWait command Brightness: {commandParamaters.waitSeconds}");

            DisplayMenuPrompt("User Programming");
            return commandParamaters;
        }


        static void GetValidInteger(string v1, int v2, int v3, out int motorSpeed)
        {
            bool validInt = false;

            do
            {
                Console.WriteLine(v1);
                bool userNum = Int32.TryParse(Console.ReadLine(), out motorSpeed);

                if (userNum == true && motorSpeed <= 255 && motorSpeed >= 1)
                {
                    validInt = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\tPlease enter an integer from '1' to '255'\n");
                }
            } while (!validInt);

        }

        static void GetValidDouble(string v1, int v2, int v3, out double waitSeconds)
        {
            bool validInt = false;
            do
            {
                Console.WriteLine(v1);
                bool validDouble = Double.TryParse(Console.ReadLine(), out waitSeconds);

                if (validDouble == false || waitSeconds < 0 || waitSeconds > 10)
                {
                    Console.Clear();
                    Console.WriteLine("\tPlease enter an integer between '0' and '10'");
                }
                else
                {
                    validInt = true;
                }
                while (!validInt) ;



            } while (!validInt);
        }
        
        /// <summary>
        /// Load user program to data
        /// </summary>
        static void LoadUserProgram()
        {
            string dataPath = @"Data/User.txt";
            string[] MotorSpeed;
            MotorSpeed = commandParamaters;

            GetValidInteger Finch Robot Commands;

            MotorSpeed = File.ReadAllLines(dataPath);

            Enum.TryParse(GetValidInteger [0], true, out commandParamaters);
          

            return (Finch Robot Commands);
        }
        
        /// <summary>
        /// write user program to data
        /// </summary>
        staic void WriteUserProgram()
        {
            string dataPath = @"Data/User.txt";

            File.WriteAllText(dataPath, Command.ToString() + "\n");
            File.AppendAllText(dataPath, Command.ToString());
        }

        ///<summary>
        ///check the user log in name
        ///</summary>
        ///<param name="userName">//get username
        ///<param name="password">//get password
        ///<return> true if valid </return>






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
#endregion;


#endregion;