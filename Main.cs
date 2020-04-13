using PiTung;
using PiTung.Components;
using PiTung.Console;
using UnityEngine;
using System;
using System.Collections.Generic;
using LWComponentsInTUNG;

public class MoreComponents : Mod {
    public override string Name => "More Components";
    public override string PackageName => "com.repsi0.morecomponents";
    public override string Author => "Repsi0";
    public override Version ModVersion => new Version("0.1");

    private static string listeningChars_alpha = "abcdefghijklmnopqrstuvwxyz";
    private static string listeningKeys_space = "space";
    private static string listeningChars_num = "1234567890";

    public static bool[] alphakey_data;
    public static bool[] numpad_data;

    public static List<KeyboardHandler> keyboards;
    public static List<NumpadHandler> numpads;

    public static void onKeyboardUpdate() {
        alphakey_data[0]=KBR.isActive("a")|KBR.isActive("c")|KBR.isActive("e")|KBR.isActive("g")|
                      KBR.isActive("i")|KBR.isActive("k")|KBR.isActive("m")|KBR.isActive("o")|
                      KBR.isActive("q")|KBR.isActive("s")|KBR.isActive("u")|KBR.isActive("w")|
                      KBR.isActive("y")|KBR.isActive("space");
        alphakey_data[1]=KBR.isActive("b")|KBR.isActive("c")|KBR.isActive("f")|KBR.isActive("g")|
                      KBR.isActive("j")|KBR.isActive("k")|KBR.isActive("n")|KBR.isActive("o")|
                      KBR.isActive("r")|KBR.isActive("s")|KBR.isActive("v")|KBR.isActive("w")|
                      KBR.isActive("z")|KBR.isActive("space");
        alphakey_data[2]=KBR.isActive("d")|KBR.isActive("e")|KBR.isActive("f")|KBR.isActive("g")|
                      KBR.isActive("l")|KBR.isActive("m")|KBR.isActive("n")|KBR.isActive("o")|
                      KBR.isActive("t")|KBR.isActive("u")|KBR.isActive("v")|KBR.isActive("w");
        alphakey_data[3]=KBR.isActive("h")|KBR.isActive("i")|KBR.isActive("j")|KBR.isActive("k")|
                      KBR.isActive("l")|KBR.isActive("m")|KBR.isActive("n")|KBR.isActive("o")|
                      KBR.isActive("x")|KBR.isActive("y")|KBR.isActive("z")|KBR.isActive("space");
        alphakey_data[4]=KBR.isActive("p")|KBR.isActive("q")|KBR.isActive("r")|KBR.isActive("s")|
                      KBR.isActive("t")|KBR.isActive("u")|KBR.isActive("v")|KBR.isActive("w")|
                      KBR.isActive("x")|KBR.isActive("y")|KBR.isActive("z")|KBR.isActive("space");

        numpad_data[0]=KBR.isActive("1")|KBR.isActive("3")|KBR.isActive("5")|KBR.isActive("7")|KBR.isActive("9");
        numpad_data[1]=KBR.isActive("2")|KBR.isActive("3")|KBR.isActive("6")|KBR.isActive("7")|KBR.isActive("0");
        numpad_data[2]=KBR.isActive("4")|KBR.isActive("5")|KBR.isActive("6")|KBR.isActive("7");
        numpad_data[3]=KBR.isActive("8")|KBR.isActive("9")|KBR.isActive("0");

        List<KeyboardHandler> kbhs_to_remove = new List<KeyboardHandler>();
        List<NumpadHandler> nps_to_remove = new List<NumpadHandler>();
        foreach(KeyboardHandler kbh in keyboards) {
            if(kbh != null && kbh.isActiveAndEnabled) {
                kbh.QueueCircuitLogicUpdate();
            } else {
                kbhs_to_remove.Add(kbh);
            }
        }
        foreach(NumpadHandler np in numpads) {
            if(np!=null&&np.isActiveAndEnabled) {
                np.QueueCircuitLogicUpdate();
            } else {
                nps_to_remove.Add(np);
            }
        }

        foreach(KeyboardHandler kbh in kbhs_to_remove) {
            keyboards.Remove(kbh);
        }
        foreach(NumpadHandler np in nps_to_remove) {
            numpads.Remove(np);
        }
    }

    public override void BeforePatch() {
        //cool misc gates
        var randomizerShape = PrefabBuilder.Cube
                         .WithIO(CubeSide.Top, SideType.Input)
                         .WithIO(CubeSide.Front, SideType.Output)
                         .WithColor(new UnityEngine.Color(1.0f, 0.553f, 0.796f));
        //base gates
        var xorShape = PrefabBuilder.Cube
                        .WithIO(CubeSide.Top, SideType.Input)
                        .WithIO(CubeSide.Back, SideType.Input)
                        .WithIO(CubeSide.Front, SideType.Output)
                        .WithColor(new UnityEngine.Color(0.280f, 0.161f, 0.586f));
        var orShape =    PrefabBuilder.Cube
                        .WithIO(CubeSide.Top, SideType.Input)
                        .WithIO(CubeSide.Back, SideType.Input)
                        .WithIO(CubeSide.Front, SideType.Output)
                        .WithColor(new UnityEngine.Color(0.280f, 0.561f, 0.286f));
        var andShape =   PrefabBuilder.Cube
                        .WithIO(CubeSide.Top, SideType.Input)
                        .WithIO(CubeSide.Back, SideType.Input)
                        .WithIO(CubeSide.Front, SideType.Output)
                        .WithColor(new UnityEngine.Color(0.580f, 0.161f, 0.286f));
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
        //math
        var addShape = PrefabBuilder.Cube //Full adder
                      .WithIO(CubeSide.Left, SideType.Input) //A
                      .WithIO(CubeSide.Right, SideType.Input) //B
                      .WithIO(CubeSide.Back, SideType.Input) //cin
                      .WithIO(CubeSide.Top, SideType.Output) //cout
                      .WithIO(CubeSide.Front, SideType.Output) //output
                      .WithColor(new UnityEngine.Color(0.827f, 0.0f, 0.232f));
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
        //false/true
        var falseTrueShape = PrefabBuilder.Cube
                        .WithIO(CubeSide.Front, SideType.Output);

        //debug
        var debugShape = PrefabBuilder.Cube //Debug Component
                        .WithIO(CubeSide.Back, SideType.Input)
                        .WithIO(CubeSide.Left, SideType.Input)
                        .WithIO(CubeSide.Front, SideType.Output)
                        .WithIO(CubeSide.Right, SideType.Output);

        ComponentRegistry.CreateNew<RandomizerHandler>("randomizer", "Randomizer", randomizerShape);

        ComponentRegistry.CreateNew<XORHandler>("xor", "XOR", xorShape);
        ComponentRegistry.CreateNew<ORHandler>("or", "OR", orShape);
        ComponentRegistry.CreateNew<ANDHandler>("and", "AND", andShape);

        ComponentRegistry.CreateNew<DFFHandler>("dff", "D-Flip-Flop", dffShape);
        ComponentRegistry.CreateNew<TFFHandler>("tff", "T-Flip-Flop", tffShape);
        ComponentRegistry.CreateNew<RSHandler>("rsnor", "RS-Latch", rsShape);
        ComponentRegistry.CreateNew<RegHandler>("reg", "Register", regShape);

        ComponentRegistry.CreateNew<KeyboardListenerHandler>("keylistener", "Keyboard Interface", keyInterfaceShape);
        ComponentRegistry.CreateNew<KeyboardHandler>("alphakey", "Keyboard", alphaKeyShape);
        ComponentRegistry.CreateNew<NumpadHandler>("numkey", "Numpad", numberKeyShape);

        ComponentRegistry.CreateNew<TrueHandler>("truegate", "TRUE", falseTrueShape);
        ComponentRegistry.CreateNew<FalseHandler>("falsegate", "FALSE", falseTrueShape);

        ComponentRegistry.CreateNew<AddHandler>("add", "Adder", addShape);

        //debug component
        //ComponentRegistry.CreateNew<DebugHandler>("debug", "MY ONLY PURPOSE IS FOR DEBUGGING PLEASE END MY PAINFUL EXISTENCE BY COMMENTING OUT MY CODE, REPSI", debugShape);

        //register keys this is the worst ever why did i do it like this
        for(int i = 0; i < listeningChars_alpha.Length; i++) {
            KBR.registerKey(""+listeningChars_alpha[i]);
        }
        KBR.registerKey(listeningKeys_space);
        for(int i = 0;i<listeningChars_num.Length;i++) {
            KBR.registerKey(""+listeningChars_num[i]);
        }

        alphakey_data = new bool[5];
        numpad_data=new bool[4];
        keyboards=new List<KeyboardHandler>();
        numpads=new List<NumpadHandler>();
}
}
public class DebugHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        Outputs[0].On=Inputs[0].On;
        Outputs[1].On=Inputs[1].On;
    }
}
public class TrueHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        Outputs[0].On=true;
    }
}
public class FalseHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        Outputs[0].On=false;
    }
}
public class KeyboardListenerHandler : UpdateHandler {
    bool lastState = false;
    protected override void CircuitLogicUpdate() {
        if(Inputs[0].On&&!lastState) {
            KBR.processKeys();
            MoreComponents.onKeyboardUpdate();
        }
        lastState=Inputs[0].On;
    }
}
public class KeyboardHandler : UpdateHandler {
    public KeyboardHandler() {
        MoreComponents.keyboards.Add(this);
    }
    protected override void CircuitLogicUpdate() {
        //ABCDEFGHIJKLMNOPQRSTUVWXYZ
        
        for(int i = 0; i < Outputs.Length; i++) {
            Outputs[i].On=MoreComponents.alphakey_data[i];
            IGConsole.Log(Outputs[i].On?1:0);
        }
    }
}
public class NumpadHandler : UpdateHandler {
    public NumpadHandler() {
        MoreComponents.numpads.Add(this);
    }
    
    protected override void CircuitLogicUpdate() {
        //0123456789
        for(int i = 0;i<Outputs.Length;i++) {
            Outputs[i].On=MoreComponents.numpad_data[i];
        }
    }
}
public class AddHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        bool firstLayerCout = Inputs[0].On&Inputs[1].On; //A & B
        bool firstAdd = Inputs[0].On^Inputs[1].On; //A XOR B
        bool secondLayerCout = firstAdd&Inputs[2].On; //XOR & Cin
        bool secondAdd = firstAdd^Inputs[2].On; //XOR XOR Cin
        Outputs[0].On=secondLayerCout|firstLayerCout;
        Outputs[1].On=secondAdd;
    }
}
public class RegHandler : UpdateHandler {
    [SaveThisAttribute]
    bool data;
    protected override void CircuitLogicUpdate() {
        if(Inputs[1].On) data=Inputs[0].On; //if writing
        if(Inputs[2].On) Outputs[0].On=data; //if reading
        else Outputs[0].On=false; //if not reading don't output lol
    }
}
public class DFFHandler : UpdateHandler {
    [SaveThisAttribute]
    bool data;
    protected override void CircuitLogicUpdate() {
        if(Inputs[1].On) data=Inputs[0].On; //set internal data to input if writing
        Outputs[0].On=data; //always = data
    }
}
public class TFFHandler : UpdateHandler {
    [SaveThisAttribute]
    bool lastState = false; //last state of the input
    protected override void CircuitLogicUpdate() {
        bool changedStateToTrue=(lastState==false)&&(Inputs[0]==true); //check if state has changed to true
        if(changedStateToTrue) { //if so,
            Outputs[0].On=!Outputs[0].On; //toggle output
        }
        lastState=Inputs[0].On; //set 
    }
}
public class RSHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        if(Inputs[1].On) Outputs[0].On=true; //set
        if(Inputs[0].On) Outputs[0].On=false; //reset //putting [0] check last b/c i'd prefer if both are on, to ultimately output false
    }
}
public class XORHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        this.Outputs[0].On=this.Inputs[1].On^this.Inputs[0].On; //xor
    }
}
public class ORHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        this.Outputs[0].On=this.Inputs[1].On|this.Inputs[0].On; //or
    }
}
public class ANDHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        this.Outputs[0].On=this.Inputs[1].On&this.Inputs[0].On; //and
    }
}
public class RandomizerHandler : UpdateHandler {
    [SaveThisAttribute]
    bool lastState; //save last input
    [SaveThisAttribute]
    bool previousOutput; //save previous output so it doesn't immediately default off the next tick
    System.Random r = null;
    protected override void CircuitLogicUpdate() {
        bool currentState = this.Inputs[0].On;
        bool changedState = (currentState!=lastState);
        bool wasFalseNowTrue = changedState&!lastState;
        bool notChangedStillTrue = lastState&currentState;

        if(wasFalseNowTrue) { //if the gate is now active
            //randomize
            if(r == null) r=new System.Random();
            previousOutput=r.Next(2)==1; //change previous output
        }
        if(currentState)
            //set to output or previous output, whichever's more recent
            this.Outputs[0].On=previousOutput;
        else
            //just set it to false
            this.Outputs[0].On=false;
        lastState=currentState;
    }
}
