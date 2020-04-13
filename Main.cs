using PiTung;
using PiTung.Components;
using System;

public class MoreComponents : Mod {
    public override string Name => "More Components";
    public override string PackageName => "com.repsi0.morecomponents";
    public override string Author => "Repsi0";
    public override Version ModVersion => new Version("0.1");

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

        ComponentRegistry.CreateNew<AddHandler>("add", "Adder", addShape);

        //debug component
        //ComponentRegistry.CreateNew<DebugHandler>("debug", "MY ONLY PURPOSE IS FOR DEBUGGING PLEASE END MY PAINFUL EXISTENCE BY COMMENTING OUT MY CODE, REPSI", debugShape);

    }
}
public class DebugHandler : UpdateHandler {
    protected override void CircuitLogicUpdate() {
        Outputs[0].On=Inputs[0].On;
        Outputs[1].On=Inputs[1].On;
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
    [SaveThis]
    bool data;
    protected override void CircuitLogicUpdate() {
        if(Inputs[1].On) data=Inputs[0].On; //if writing
        if(Inputs[2].On) Outputs[0].On=data; //if reading
        else Outputs[0].On=false; //if not reading then don't output
    }
}
public class DFFHandler : UpdateHandler {
    [SaveThis]
    bool data;
    protected override void CircuitLogicUpdate() {
        if(Inputs[1].On) data=Inputs[0].On; //set internal data to input if writing
        Outputs[0].On=data; //always = data
    }
}
public class TFFHandler : UpdateHandler {
    [SaveThis]
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
    [SaveThis]
    bool lastState; //save last input
    [SaveThis]
    bool previousOutput; //save previous output so it doesn't immediately default off the next tick
    Random r = null;
    protected override void CircuitLogicUpdate() {
        bool currentState = this.Inputs[0].On;
        bool changedState = (currentState!=lastState);
        bool wasFalseNowTrue = changedState&!lastState;
        bool notChangedStillTrue = lastState&currentState;

        if(wasFalseNowTrue) { //if the gate is now active
            //randomize
            if(r == null) r=new Random();
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
