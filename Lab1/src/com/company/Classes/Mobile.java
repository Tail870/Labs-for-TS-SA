package com.company.Classes;

public class Mobile extends Desktop {
    public String model;
    public String cellular;

    public Mobile() {
        isMobile = true;
    }

    @Override
    public String display() {
        return "Name: " + name + "\n" +
                "Model: " + model + "\n" +
                "Cellular: " + cellular + "\n" +
                "Architecture: " + getArch() + "\n" +
                "Address: " + address + "\n" +
                "Whose: " + whose;
    }

    @Override
    public String toString() {
        return display();
    }
}

