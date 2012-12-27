using System;
using System.DirectoryServices;
using NUnit.Framework;

namespace Manager.Ui.Tests
{
    [TestFixture]
    public class ActiveDirectorySearchTests
    {
        [Test]
        public void Can_read_current_user_primary_email_address()
        {   // http://lozanotek.com/blog/articles/149.aspx
            // Get the username and domain information
            var userName = Environment.UserName;
            var domainName = Environment.UserDomainName;

            // Set the correct format for the AD query and filter
            var rootQuery = string.Format(@"LDAP://{0}", domainName);
            //var rootQuery = string.Format(@"LDAP://{0}.com/DC={0},DC=com", domainName);
            var searchFilter = string.Format(@"(&(samAccountName={0})(objectCategory=person)(objectClass=user))", userName);

            SearchResult result = null;
            using (var root = new DirectoryEntry(rootQuery))
            {
                using (var searcher = new DirectorySearcher(root))
                {
                    searcher.Filter = searchFilter;
                    result = searcher.FindOne();
                }
            }
            Assert.IsNotNull(result);

            // Get user primary email address
            var primaryEmail = result.Properties["mail"][0] as string;
            Assert.IsNotNull(primaryEmail);
        }
    }
}
