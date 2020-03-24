package com.company.Classes;

import java.util.Scanner;

public abstract class Computer {
    enum Arch {
        x86,
        x86_64,
        arm,
        arm64
    }

    public String name;
    private Arch arch;
    public String address;

    String getArch() {
        return arch.toString();
    }

    public void setArch() {
        Scanner sc = new Scanner(System.in);
        char menu;
        do {
            System.out.println("1. x86;");
            System.out.println("2. x86_64;");
            System.out.println("3. arm;");
            System.out.println("4. arm64.");
            menu = sc.nextLine().charAt(0);
            switch (menu) {
                case '1': {
                    arch = Arch.x86;
                    return;
                }
                case '2': {
                    arch = Arch.x86_64;
                    return;
                }
                case '3': {
                    arch = Arch.arm;
                    return;
                }
                case '4': {
                    arch = Arch.arm64;
                    return;
                }
            }
        } while (true);
    }

    public abstract String display();
}
