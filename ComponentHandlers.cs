using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiTung.Components;

namespace LWComponentsInTUNG {
    public class ArrowKeyHandler : UpdateHandler {
        public ArrowKeyHandler() {
            MoreComponents.arrowkeys.Add(this);
        }
        protected override void CircuitLogicUpdate() {
            for(int i = 0;i<Outputs.Length;i++) {
                Outputs[i].On=MoreComponents.arrowkey_data[i];
            }
        }
    }
    public class WASDHandler : UpdateHandler {
        public WASDHandler() {
            MoreComponents.wasds.Add(this);
        }
        protected override void CircuitLogicUpdate() {
            for(int i = 0;i<Outputs.Length;i++) {
                Outputs[i].On=MoreComponents.wasd_data[i];
            }
        }
    }
    public class SpacebarHandler : UpdateHandler {
        public SpacebarHandler() {
            MoreComponents.spacebars.Add(this);
        }
        protected override void CircuitLogicUpdate() {
            Outputs[0].On=MoreComponents.spacebar_data;
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

            for(int i = 0;i<Outputs.Length;i++) {
                Outputs[i].On=MoreComponents.alphakey_data[i];
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
            bool changedStateToTrue = (lastState==false)&&(Inputs[0].On==true); //check if state has changed to true
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
                if(r==null) r=new System.Random();
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
    public class ControlKeyHandler : UpdateHandler {
        public ControlKeyHandler() {
            MoreComponents.controlkeys.Add(this);
        }
        protected override void CircuitLogicUpdate() {
            for(int i = 0;i<Outputs.Length;i++) {
                Outputs[i].On=MoreComponents.controlkey_data[i];
            }
        }
    }
    public class ShiftKeyHandler : UpdateHandler {
        public ShiftKeyHandler() {
            MoreComponents.shiftkeys.Add(this);
        }
        protected override void CircuitLogicUpdate() {
            for(int i = 0;i<Outputs.Length;i++) {
                Outputs[i].On=MoreComponents.shiftkey_data[i];
            }
        }
    }
    public class AltKeyHandler : UpdateHandler {
        public AltKeyHandler() {
            MoreComponents.altkeys.Add(this);
        }
        protected override void CircuitLogicUpdate() {
            for(int i = 0;i<Outputs.Length;i++) {
                Outputs[i].On=MoreComponents.altkey_data[i];
            }
        }
    }
    public class EnterKeyHandler : UpdateHandler {
        public EnterKeyHandler() {
            MoreComponents.enterkeys.Add(this);
        }
        protected override void CircuitLogicUpdate() {
            for(int i = 0;i<Outputs.Length;i++) {
                Outputs[i].On=MoreComponents.enterkey_data[i];
            }
        }
    }
}
