package com.company.Classes;

public class Server extends Computer {
    public String purpose;
    public String company;

    @Override
    public String display() {
        return "Name: " + name + "\n" +
                "Architecture: " + getArch() + "\n" +
                "Address: " + address + "\n" +
                "Company: " + company + "\n" +
                "Purpose: " + purpose;
    }

    @Override
    public String toString() {
        return display();
    }
}
