using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiTung;
using PiTung.Components;

namespace LWComponentsInTUNG {
    class ComponentRegistrar {
        public static void RegisterComponents() {
            RegisterCoolGates();
            RegisterBaseGates();
            RegisterMemComponents();
            RegisterIOComponents();
            //RegisterDebug();  //COMMENT ME OUT PLEASE IM BEGGING YOU END MY CEASELESS PAIN AND SUFFERING
        }

        static void RegisterCoolGates() {
            //cool misc gates
            var randomizerShape = PrefabBuilder.Cube
                             .WithIO(CubeSide.Top, SideType.Input)
                             .WithIO(CubeSide.Front, SideType.Output)
                             .WithColor(new UnityEngine.Color(1.0f, 0.553f, 0.796f));

            ComponentRegistry.CreateNew<RandomizerHandler>("randomizer", "Randomizer", randomizerShape);
        }

        static void RegisterBaseGates() {
            //base gates
            var xorShape = PrefabBuilder.Cube
                            .WithIO(CubeSide.Top, SideType.Input)
                            .WithIO(CubeSide.Back, SideType.Input)
                            .WithIO(CubeSide.Front, SideType.Output)
                            .WithColor(new UnityEngine.Color(0.280f, 0.161f, 0.586f));
            var orShape = PrefabBuilder.Cube
                            .WithIO(CubeSide.Top, SideType.Input)
                            .WithIO(CubeSide.Back, SideType.Input)
                            .WithIO(CubeSide.Front, SideType.Output)
                            .WithColor(new UnityEngine.Color(0.280f, 0.561f, 0.286f));
            var andShape = PrefabBuilder.Cube
                            .WithIO(CubeSide.Top, SideType.Input)
                            .WithIO(CubeSide.Back, SideType.Input)
                            .WithIO(CubeSide.Front, SideType.Output)
                            .WithColor(new UnityEngine.Color(0.580f, 0.161f, 0.286f));
            //false/true
            var falseTrueShape = PrefabBuilder.Cube
                            .WithIO(CubeSide.Front, SideType.Output);

            ComponentRegistry.CreateNew<TrueHandler>("truegate", "TRUE", falseTrueShape);
            ComponentRegistry.CreateNew<FalseHandler>("falsegate", "FALSE", falseTrueShape);
            ComponentRegistry.CreateNew<XORHandler>("xor", "XOR", xorShape);
            ComponentRegistry.CreateNew<ORHandler>("or", "OR", orShape);
            ComponentRegistry.CreateNew<ANDHandler>("and", "AND", andShape);
        }

        static void RegisterMemComponents() {
            //memory components
            var dffShape = PrefabBuilder.Cube //D-flip-flop
                          .WithIO(CubeSide.Left, SideType.Input) //data
                          .WithIO(CubeSide.Top, SideType.Input) //write
                          .WithIO(CubeSide.Front, SideType.Output) //out
                          .WithColor(new UnityEngine.Color(0.827f, 0.467f, 0.0f));
            var tffShape = PrefabBuilder.Cube //T-flip-flop
                          .WithIO(CubeSide.Top, SideType.Input) //input
                          .WithIO(CubeSide.Front, SideType.Output)
                          .WithColor(new UnityEngine.Color(0.467f, 0.827f, 0.0f));
            var rsShape = PrefabBuilder.Cube //RS-latch
                          .WithIO(CubeSide.Back, SideType.Input) //set
                          .WithIO(CubeSide.Top, SideType.Input) //reset
                          .WithIO(CubeSide.Front, SideType.Output)
                          .WithColor(new UnityEngine.Color(0.467f, 0.0f, 0.827f));
            var regShape = PrefabBuilder.Cube //Register (DFF + read)
                          .WithIO(CubeSide.Left, SideType.Input) //data
                          .WithIO(CubeSide.Top, SideType.Input) //write
                          .WithIO(CubeSide.Back, SideType.Input) //read
                          .WithIO(CubeSide.Front, SideType.Output) //output
                          .WithColor(new UnityEngine.Color(0.827f, 0.467f, 0.0f));

            ComponentRegistry.CreateNew<DFFHandler>("dff", "D-Flip-Flop", dffShape);
            ComponentRegistry.CreateNew<TFFHandler>("tff", "T-Flip-Flop", tffShape);
            ComponentRegistry.CreateNew<RSHandler>("rsnor", "RS-Latch", rsShape);
            ComponentRegistry.CreateNew<RegHandler>("reg", "Register", regShape);
        }

        static void RegisterIOComponents() {
            //io
            //0.596f, 0.424f, 0.263f
            var alphaKeyShape = PrefabBuilder.Cube //alphabet key
                               .WithIO(CubeSide.Left, SideType.Output) //1
                               .WithIO(CubeSide.Front, SideType.Output) //2
                               .WithIO(CubeSide.Right, SideType.Output) //4
                               .WithIO(CubeSide.Back, SideType.Output) //8
                               .WithIO(CubeSide.Top, SideType.Output) //16
                               .WithColor(new UnityEngine.Color(0.596f, 0.424f, 0.263f));
            var numberKeyShape = PrefabBuilder.Cube //number key
                                .WithIO(CubeSide.Left, SideType.Output) //1
                                .WithIO(CubeSide.Front, SideType.Output) //2
                                .WithIO(CubeSide.Right, SideType.Output) //4
                                .WithIO(CubeSide.Back, SideType.Output) //8
                                .WithColor(new UnityEngine.Color(0.424f, 0.424f, 0.596f));
            var keyInterfaceShape = PrefabBuilder.Cube //allows player to send a signal to it to listen to keys
                                   .WithIO(CubeSide.Top, SideType.Input)
                                   .WithColor(new UnityEngine.Color(0.424f, 0.596f, 0.424f));
            var arrowKeyShape = PrefabBuilder.Cube
                               .WithIO(CubeSide.Top, SideType.Output)
                               .WithIO(CubeSide.Left, SideType.Output)
                               .WithIO(CubeSide.Front, SideType.Output)
                               .WithIO(CubeSide.Right, SideType.Output)
                               .WithColor(new UnityEngine.Color(204f/255f, 199f/255f, 72f/255f));
            var spacebarShape = PrefabBuilder.Cube
                               .WithIO(CubeSide.Front, SideType.Output)
                               .WithColor(new UnityEngine.Color(204f/255f, 199f/255f, 72f/255f));
            var controlKeyShape = PrefabBuilder.Cube
                                 .WithIO(CubeSide.Left, SideType.Output)
                                 .WithIO(CubeSide.Right, SideType.Output)
                                 .WithColor(new UnityEngine.Color(185f/255f,100f/255f, 25f/255f));
            
            ComponentRegistry.CreateNew<KeyboardListenerHandler>("keylistener", "Keyboard Interface", keyInterfaceShape);
            ComponentRegistry.CreateNew<KeyboardHandler>("alphakey", "Keyboard", alphaKeyShape);
            ComponentRegistry.CreateNew<NumpadHandler>("numkey", "Numpad", numberKeyShape);
            ComponentRegistry.CreateNew<ArrowKeyHandler>("arrowkey", "Arrow Keys", arrowKeyShape);
            ComponentRegistry.CreateNew<WASDHandler>("wasd", "WASD Keys", arrowKeyShape);
            ComponentRegistry.CreateNew<SpacebarHandler>("spacebar", "Spacebar", spacebarShape);
            ComponentRegistry.CreateNew<ControlKeyHandler>("ctrlkey", "Control", controlKeyShape);
            ComponentRegistry.CreateNew<ShiftKeyHandler>("shiftkey", "Shift", controlKeyShape);
            ComponentRegistry.CreateNew<AltKeyHandler>("altkey", "Alt", controlKeyShape);
            ComponentRegistry.CreateNew<EnterKeyHandler>("enterkey", "Enter", controlKeyShape);

        }

        static void RegisterMathComponents() {
            //math
            var addShape = PrefabBuilder.Cube //Full adder
                          .WithIO(CubeSide.Left, SideType.Input) //A
                          .WithIO(CubeSide.Right, SideType.Input) //B
                          .WithIO(CubeSide.Back, SideType.Input) //cin
                          .WithIO(CubeSide.Top, SideType.Output) //cout
                          .WithIO(CubeSide.Front, SideType.Output) //output
                          .WithColor(new UnityEngine.Color(0.827f, 0.0f, 0.232f));

            ComponentRegistry.CreateNew<AddHandler>("add", "Adder", addShape);
        }

        static void RegisterDebug() {
            //debug
            var debugShape = PrefabBuilder.Cube //Debug Component
                            .WithIO(CubeSide.Back, SideType.Input)
                            .WithIO(CubeSide.Left, SideType.Input)
                            .WithIO(CubeSide.Front, SideType.Output)
                            .WithIO(CubeSide.Right, SideType.Output);
            ComponentRegistry.CreateNew<DebugHandler>("debug", "MY ONLY PURPOSE IS FOR DEBUGGING PLEASE END MY PAINFUL EXISTENCE BY COMMENTING OUT MY CODE, REPSI", debugShape);
        }
    }
}
