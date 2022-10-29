class Notification
{
    public int amount { get; set; }

    public Notification()
    {
        // Default constructor
        this.amount = 0;
    }

    public Notification(int amount)
    {
        this.amount = amount;
    }
}