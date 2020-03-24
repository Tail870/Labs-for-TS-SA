package com.company;

import com.company.Classes.Computer;
import com.company.Classes.Desktop;
import com.company.Classes.Mobile;
import com.company.Classes.Server;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.Scanner;

public class Main {

    private static ArrayList<Computer> computers = new ArrayList<>();

    public static void main(String[] args) {
        Scanner sc = new Scanner(System.in);
        char menu;
        do {
            System.out.println("1. Create an object;");
            System.out.println("2. Display objects;");
            System.out.println("3. Search and display objects;");
            System.out.println("0. Exit.");
            menu = sc.nextLine().charAt(0);
            switch (menu) {
                case '1': {
                    create();
                    break;
                }
                case '2': {
                    display();
                    break;
                }
                case '3': {
                    search();
                    break;
                }
            }
        } while (menu != '0' & menu != 'O' & menu != 'o');
    }

    private static void create() {
        Scanner sc = new Scanner(System.in);
        char menu;
        do {
            System.out.println("1. Create a Desktop;");
            System.out.println("2. Create a Mobile;");
            System.out.println("3. Create a Server;");
            System.out.println("0. Exit.");
            menu = sc.nextLine().charAt(0);
            switch (menu) {
                case '1': {
                    Desktop desktop = new Desktop();
                    System.out.print("Name: ");
                    desktop.name = sc.nextLine();
                    System.out.print("Architecture:\n");
                    desktop.setArch();
                    System.out.print("Address: ");
                    desktop.address = sc.nextLine();
                    System.out.print("Whose: ");
                    desktop.whose = sc.nextLine();
                    computers.add(desktop);
                    return;
                }
                case '2': {
                    Mobile mobile = new Mobile();
                    System.out.print("Name: ");
                    mobile.name = sc.nextLine();
                    System.out.print("Model: ");
                    mobile.model = sc.nextLine();
                    System.out.print("Cellular: ");
                    mobile.cellular = sc.nextLine();
                    System.out.print("Architecture:\n");
                    mobile.setArch();
                    System.out.print("Address: ");
                    mobile.address = sc.nextLine();
                    System.out.print("Whose: ");
                    mobile.whose = sc.nextLine();
                    computers.add(mobile);
                    return;
                }
                case '3': {
                    Server server = new Server();
                    System.out.print("Name: ");
                    server.name = sc.nextLine();
                    System.out.print("Architecture:\n");
                    server.setArch();
                    System.out.print("Address: ");
                    server.address = sc.nextLine();
                    System.out.print("Company: ");
                    server.company = sc.nextLine();
                    System.out.print("Purpose: ");
                    server.purpose = sc.nextLine();
                    computers.add(server);
                    return;
                }
            }
        } while (menu != '0' & menu != 'O' & menu != 'o');
    }

    private static void display() {
        System.out.println(computers);
    }

    private static void search() {
        Scanner sc = new Scanner(System.in);
        char menu;
        do {
            System.out.println("1. Search for specific object;");
            System.out.println("2. Contains a string;");
            System.out.println("0. Exit.");
            menu = sc.nextLine().charAt(0);
            switch (menu) {
                case '1': {
                    searchFor();
                    break;
                }
                case '2': {
                    System.out.println("String to search: ");
                    String search = sc.nextLine();
                    Iterator iter = computers.iterator();
                    String temp;
                    while (iter.hasNext()) {
                        temp = iter.next().toString();
                        if (temp.contains(search))
                            System.out.println(temp + "\n");
                    }
                    break;
                }
            }
            System.out.println("\n--------End search.");
        }
        while (menu != '0' & menu != 'O' & menu != 'o');
    }

    private static void searchFor() {
        Scanner sc = new Scanner(System.in);
        char menu;
        do {
            System.out.println("1. Search for desktop;");
            System.out.println("2. Search for mobile;");
            System.out.println("3. Search for server;");
            System.out.println("0. Exit.");
            menu = sc.nextLine().charAt(0);
            Iterator iter = computers.iterator();
            Computer temp;
            switch (menu) {
                case '1': {
                    while (iter.hasNext()) {
                        temp = (Computer) iter.next();
                        if (temp instanceof Desktop) {
                            if (!((Desktop) temp).isMobile)
                                System.out.println(temp.toString());
                        }
                    }
                    break;
                }
                case '2': {
                    while (iter.hasNext()) {
                        temp = (Computer) iter.next();
                        if (temp instanceof Mobile) {
                            if (((Desktop) temp).isMobile)
                                System.out.println(temp.toString());
                        }
                    }
                    break;
                }
                case '3': {
                    while (iter.hasNext()) {
                        temp = (Computer) iter.next();
                        if (temp instanceof Server)
                            System.out.println(temp.toString());
                    }
                    break;
                }
            }
            System.out.println("\n--------End search.");
        }
        while (menu != '0' & menu != 'O' & menu != 'o');
    }
}