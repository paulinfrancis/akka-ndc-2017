using System;
using Akka.Actor;
using Akka.Configuration;

namespace AkkaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var ip = "localhost";
            if (IsRunningOnMono())
            {
                ip = "raspberrypi.local";
                Console.WriteLine($"Hello NDC from Mono on the RPi!\n\nLet's deploy some actors to {ip}! ");
            }
            else
            {
                Console.WriteLine($"Hello NDC from .NET!\n\nLet's deploy some actors to {ip}!");
            }

            var actorSystem = ActorSystem.Create("PiActorSystem", ConfigurationFactory.ParseString(@"
                akka {
                    actor {
                        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                        serializers {
                            hyperion = ""Akka.Serialization.HyperionSerializer, Akka.Serialization.Hyperion""
                        }
                        serialization-bindings {
                            ""System.Object"" = hyperion
                        }
                    }
                    remote {
                        helios.tcp {
                            port = 8090
                            hostname =" + ip + @"
                        }
                    }
                }
            "));

            Console.ReadLine();
        }

        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
    }
}
