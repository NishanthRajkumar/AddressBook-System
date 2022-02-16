namespace AddressBookSystem;

// This class handles each individual contact
internal class Contact
{
    // Declared attributes required for a person's contact
    private string firstName = "";
    private string lastName = "";
    private string email = "";
    private string phone = "";
    private string city = "";
    private string state = "";
    private string zip = "";
    private string address = "";

    // Default Constructor: Ensures there is user input
    public Contact()
    {
        GetContactInfo();
    }

    // Parameterised Constructor: Takes first name & phone in parameters
    public Contact(string firstName, string phone)
    {
        this.firstName = firstName;
        if (UserInput.PhoneCheck(phone))
            this.phone = phone;
    }

    // Parameterised Constructor: Takes first name as parameter
    public Contact(string firstName)
    {
        this.firstName = firstName;
    }

    // Gets contact info from user
    private void GetContactInfo()
    {
        firstName = UserInput.GetName("First name: ");
        Console.Write("Last Name: ");
        lastName = UserInput.ReadString();
        Console.Write("Email: ");
        email = UserInput.ReadString();
        phone = UserInput.GetNumber("Phone: ");
        Console.Write("City: ");
        city = UserInput.ReadString();
        Console.Write("State: ");
        state = UserInput.ReadString();
        zip = UserInput.GetZip("Zip: ");
        Console.Write("Address: ");
        address = UserInput.ReadString();
    }

    // Displays full contact 
    public void Display()
    {
        Console.WriteLine("\nName: " + firstName + " " + lastName);
        Console.WriteLine("Email: " + email);
        Console.WriteLine("Phone: " + phone);
        Console.WriteLine("City: " + city);
        Console.WriteLine("State: " + state);
        Console.WriteLine("Zip: " + zip);
        Console.WriteLine("Address: " + address);
    }

    // Return the full name
    public string GetName()
    {
        if (String.IsNullOrEmpty(lastName) is true)
            return firstName;
        return firstName + " " + lastName;
    }
}