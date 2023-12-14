using System;
using System.Net.Mail;
using Domain.SeedWork;

namespace Ordering.Domain.Aggregates.Buyer;

public class Buyer : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Buyer(string name, string email)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        try
        {
            Email = new MailAddress(email).Address;
        }
        catch (Exception)
        {
            throw new DomainException($"{email} is not a valid email address");
        }
    }
}