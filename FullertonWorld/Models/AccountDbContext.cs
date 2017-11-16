using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RegistrationLogin.Models
{
    public class AccountDbContext : DbContext
    {
        public DbSet<UserAccount> userAccounts { get; set; }
        public DbSet<UserActivation> userActivation { get; set; }
        public DbSet<RoommateDatabase> rommateDatabase { get; set; }
        public DbSet<BuySellDatabase> buySellDatabase { get; set; }
        public DbSet<Message> messaging { get; set; }
        public DbSet<InboxViewModelDTO> userInbox { get; set; }
    }
}