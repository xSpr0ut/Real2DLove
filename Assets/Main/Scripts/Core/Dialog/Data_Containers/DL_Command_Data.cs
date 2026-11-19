<<<<<<< HEAD
using System.Collections.Generic;
using System.Text;

namespace Dialog{

    public class DL_Command_Data{
        
        public List<Command> commands;
        private const char commandSplitter300 = ',';
        private const char argumentsContainerID = '(';
        private const string waitCommandID = "[wait]";

        public struct Command{

            public string name;
            public string[] arguments;
            public bool waitForComplete;

        }

        public DL_Command_Data(string rawCommands){

            commands = RipCommands(rawCommands);

        }

        private List<Command> RipCommands(string rawCommands){

            string[] data = rawCommands.Split(commandSplitter300, System.StringSplitOptions.RemoveEmptyEntries);
            List<Command> result = new List<Command>();

            foreach(string comm in data){

                Command command = new Command();
                int index = comm.IndexOf(argumentsContainerID);
                command.name = comm.Substring(0, index).Trim();

                if(command.name.ToLower().StartsWith(waitCommandID)){

                    command.name = command.name.Substring(waitCommandID.Length);
                    command.waitForComplete = true;

                }else{

                    command.waitForComplete = false;

                }

                command.arguments = Get_Arguments(comm.Substring(index +1, comm.Length - index - 2));
                result.Add(command);

            }

            return result;

        }

        private string[] Get_Arguments(string args){

            List<string> argumentList = new List<string>();
            StringBuilder currentArgument = new StringBuilder();
            bool inQuotes = false;

            for(int i = 0; i < args.Length; i++){

                if(args[i] == '"'){

                    inQuotes = !inQuotes;
                    continue;

                }

                if(!inQuotes && args[i] == ' '){

                    argumentList.Add(currentArgument.ToString());
                    currentArgument.Clear();
                    continue;

                }

                currentArgument.Append(args[i]);

            }

            if(currentArgument.Length > 0){

                argumentList.Add(currentArgument.ToString());

            }

            return argumentList.ToArray();

        }

    }

}
=======
using System.Collections.Generic;
using System.Text;

namespace Dialog{

    public class DL_Command_Data{
        
        public List<Command> commands;
        private const char commandSplitter300 = ',';
        private const char argumentsContainerID = '(';
        private const string waitCommandID = "[wait]";

        public struct Command{

            public string name;
            public string[] arguments;
            public bool waitForComplete;

        }

        public DL_Command_Data(string rawCommands){

            commands = RipCommands(rawCommands);

        }

        private List<Command> RipCommands(string rawCommands){

            string[] data = rawCommands.Split(commandSplitter300, System.StringSplitOptions.RemoveEmptyEntries);
            List<Command> result = new List<Command>();

            foreach(string comm in data){

                Command command = new Command();
                int index = comm.IndexOf(argumentsContainerID);
                command.name = comm.Substring(0, index).Trim();

                if(command.name.ToLower().StartsWith(waitCommandID)){

                    command.name = command.name.Substring(waitCommandID.Length);
                    command.waitForComplete = true;

                }else{

                    command.waitForComplete = false;

                }

                command.arguments = Get_Arguments(comm.Substring(index +1, comm.Length - index - 2));
                result.Add(command);

            }

            return result;

        }

        private string[] Get_Arguments(string args){

            List<string> argumentList = new List<string>();
            StringBuilder currentArgument = new StringBuilder();
            bool inQuotes = false;

            for(int i = 0; i < args.Length; i++){

                if(args[i] == '"'){

                    inQuotes = !inQuotes;
                    continue;

                }

                if(!inQuotes && args[i] == ' '){

                    argumentList.Add(currentArgument.ToString());
                    currentArgument.Clear();
                    continue;

                }

                currentArgument.Append(args[i]);

            }

            if(currentArgument.Length > 0){

                argumentList.Add(currentArgument.ToString());

            }

            return argumentList.ToArray();

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
