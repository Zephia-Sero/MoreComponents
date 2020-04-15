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

    public static bool[] alphakey_data;
    public static bool[] numpad_data;
    public static bool[] wasd_data;
    public static bool[] arrowkey_data;
    public static bool spacebar_data;
    public static bool[] controlkey_data;
    public static bool[] shiftkey_data;
    public static bool[] altkey_data;
    public static bool[] enterkey_data;

    public static List<KeyboardHandler> keyboards;
    public static List<NumpadHandler> numpads;
    public static List<ArrowKeyHandler> arrowkeys;
    public static List<WASDHandler> wasds;
    public static List<SpacebarHandler> spacebars;
    public static List<ControlKeyHandler> controlkeys;
    public static List<ShiftKeyHandler> shiftkeys;
    public static List<AltKeyHandler> altkeys;
    public static List<EnterKeyHandler> enterkeys;

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

        wasd_data[0]=KBR.isActive("w");
        wasd_data[1]=KBR.isActive("a");
        wasd_data[2]=KBR.isActive("s");
        wasd_data[3]=KBR.isActive("d");

        arrowkey_data[0]=KBR.isActive("up");
        arrowkey_data[1]=KBR.isActive("left");
        arrowkey_data[2]=KBR.isActive("down");
        arrowkey_data[3]=KBR.isActive("right");

        spacebar_data=KBR.isActive("space");

        controlkey_data[0]=KBR.isActive("left ctrl");
        controlkey_data[1]=KBR.isActive("right ctrl");

        shiftkey_data[0]=KBR.isActive("left shift");
        shiftkey_data[1]=KBR.isActive("right shift");

        altkey_data[0]=KBR.isActive("left alt");
        altkey_data[1]=KBR.isActive("right alt");

        enterkey_data[0]=KBR.isActive("return");
        enterkey_data[1]=KBR.isActive("enter");

        List<KeyboardHandler> kbhs_to_remove = new List<KeyboardHandler>();
        List<NumpadHandler> nps_to_remove = new List<NumpadHandler>();
        List<ArrowKeyHandler> arrowkeys_to_remove = new List<ArrowKeyHandler>();
        List<WASDHandler> wasds_to_remove = new List<WASDHandler>();
        List<SpacebarHandler> spacebars_to_remove = new List<SpacebarHandler>();
        List<ControlKeyHandler> ctrl_to_remove = new List<ControlKeyHandler>();
        List<ShiftKeyHandler> shift_to_remove = new List<ShiftKeyHandler>();
        List<AltKeyHandler> aks_to_remove = new List<AltKeyHandler>();
        List<EnterKeyHandler> eks_to_remove = new List<EnterKeyHandler>();

        foreach(KeyboardHandler kbh in keyboards) {
            if(kbh != null && kbh.isActiveAndEnabled) {
                kbh.QueueCircuitLogicUpdate();
            } else {
                kbhs_to_remove.Add(kbh);
            }
        }
        foreach(NumpadHandler handler in numpads) {
            if(handler!=null&&handler.isActiveAndEnabled) {
                handler.QueueCircuitLogicUpdate();
            } else {
                nps_to_remove.Add(handler);
            }
        }
        foreach(ArrowKeyHandler handler in arrowkeys) {
            if(handler!=null&&handler.isActiveAndEnabled) {
                handler.QueueCircuitLogicUpdate();
            } else {
                arrowkeys_to_remove.Add(handler);
            }
        }
        foreach(WASDHandler handler in wasds) {
            if(handler!=null&&handler.isActiveAndEnabled) {
                handler.QueueCircuitLogicUpdate();
            } else {
                wasds_to_remove.Add(handler);
            }
        }
        foreach(SpacebarHandler handler in spacebars) {
            if(handler!=null&&handler.isActiveAndEnabled) {
                handler.QueueCircuitLogicUpdate();
            } else {
                spacebars_to_remove.Add(handler);
            }
        }
        foreach(ControlKeyHandler handler in controlkeys) {
            if(handler!=null&&handler.isActiveAndEnabled) {
                handler.QueueCircuitLogicUpdate();
            } else {
                ctrl_to_remove.Add(handler);
            }
        }
        foreach(ShiftKeyHandler handler in shiftkeys) {
            if(handler!=null&&handler.isActiveAndEnabled) {
                handler.QueueCircuitLogicUpdate();
            } else {
                shift_to_remove.Add(handler);
            }
        }
        foreach(AltKeyHandler handler in altkeys) {
            if(handler!=null&&handler.isActiveAndEnabled) {
                handler.QueueCircuitLogicUpdate();
            } else {
                aks_to_remove.Add(handler);
            }
        }
        foreach(EnterKeyHandler handler in enterkeys) {
            if(handler!=null&&handler.isActiveAndEnabled) {
                handler.QueueCircuitLogicUpdate();
            } else {
                eks_to_remove.Add(handler);
            }
        }

        foreach(KeyboardHandler kbh in kbhs_to_remove) {
            keyboards.Remove(kbh);
        }
        foreach(NumpadHandler np in nps_to_remove) {
            numpads.Remove(np);
        }
        foreach(ArrowKeyHandler ak in arrowkeys_to_remove) {
            arrowkeys.Remove(ak);
        }
        foreach(WASDHandler ak in wasds_to_remove) {
            wasds.Remove(ak);
        }
        foreach(SpacebarHandler ak in spacebars_to_remove) {
            spacebars.Remove(ak);
        }
        foreach(ControlKeyHandler handler in ctrl_to_remove) {
            controlkeys.Remove(handler);
        }
        foreach(ShiftKeyHandler handler in shift_to_remove) {
            shiftkeys.Remove(handler);
        }
        foreach(AltKeyHandler handler in aks_to_remove) {
            altkeys.Remove(handler);
        }
        foreach(EnterKeyHandler handler in eks_to_remove) {
            enterkeys.Remove(handler);
        }
    }

    public override void BeforePatch() {
        ComponentRegistrar.RegisterComponents();

        alphakey_data = new bool[5];
        numpad_data=new bool[4];
        arrowkey_data=new bool[4];
        wasd_data=new bool[4];
        spacebar_data=new bool();
        controlkey_data=new bool[2];
        altkey_data=new bool[2];
        shiftkey_data=new bool[2];
        enterkey_data=new bool[2];
        keyboards=new List<KeyboardHandler>();
        numpads=new List<NumpadHandler>();
        arrowkeys=new List<ArrowKeyHandler>();
        wasds=new List<WASDHandler>();
        spacebars=new List<SpacebarHandler>();
        controlkeys=new List<ControlKeyHandler>();
        altkeys=new List<AltKeyHandler>();
        shiftkeys=new List<ShiftKeyHandler>();
        enterkeys=new List<EnterKeyHandler>();
        RegisterAllKeys();
    }

    private const string listeningChars_alpha = "abcdefghijklmnopqrstuvwxyz";
    private const string listeningKeys_space = "space";
    private const string listeningChars_num = "1234567890";

    public static void RegisterAllKeys() {
        //register keys this is the worst ever why did i do it like this
        for(int i = 0;i<listeningChars_alpha.Length;i++) {
            KBR.registerKey(""+listeningChars_alpha[i]);
        }
        KBR.registerKey(listeningKeys_space);
        for(int i = 0;i<listeningChars_num.Length;i++) {
            KBR.registerKey(""+listeningChars_num[i]);
        }
        KBR.registerKey("up");
        KBR.registerKey("left");
        KBR.registerKey("down");
        KBR.registerKey("right");
        KBR.registerKey("left ctrl");
        KBR.registerKey("right ctrl");
        KBR.registerKey("left shift");
        KBR.registerKey("right shift");
        KBR.registerKey("left alt");
        KBR.registerKey("right alt");
        KBR.registerKey("enter");
        KBR.registerKey("return");
    }
}
