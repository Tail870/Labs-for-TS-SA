package com.company.Classes;

public class Desktop extends Computer {
    public String whose;
    public boolean isMobile = false;

    @Override
    public String display() {
        return "Name: " + name + "\n" +
                "Architecture: " + getArch() + "\n" +
                "Address: " + address + "\n" +
                "Whose: " + whose;
    }

    @Override
    public String toString() {
        return display();
    }
}
