﻿using AddressBookSystem;

Console.Title = "AddressBook System";
Console.WriteLine("----------AddressBook System----------");

// Creating Contact via AddressBook
Console.WriteLine("Creating new Contact in AddressBook");
AddressBook myContacts = new();
myContacts.CreateContact();
myContacts.Display();

// Adding Contact to AddressBook
Console.WriteLine("Adding new Contact in AddressBook");
Contact contact = new Contact("Nishanth", "+918842549729");
myContacts.AddContact(contact);
myContacts.Display();

// Edit contact in AddressBook
myContacts.EditContact();
myContacts.Display();

Console.ReadKey();
