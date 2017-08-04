using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Web.Security;

namespace Tienda.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            userIdentity.AddClaim(new Claim("FullName", this.FirstName + " " + this.LastName));
            return userIdentity;
        }
    }

    [Table("AspNetPermisionTypes")]
    public class PermisionType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Permision> Permisions { get; set; }

    }
    [Table("AspNetPermisions")]
    public class Permision
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PermisionTypeID { get; set; }
        public int? ParentID { get; set; }

        [ForeignKey("PermisionTypeID")]
        public virtual PermisionType PermisionType { get; set; }
        
        public virtual ICollection<RolePermision> RolePermisions { get; set; }

        [ForeignKey("ParentID")]
        public virtual Permision Parent { get; set; }

        public virtual ICollection<Permision> Childs { get; set; }
    }

    [Table("AspNetRolePermisions")]
    public class RolePermision
    {
        public string RoleId { get; set; }
        public int PermisionId { get; set; }


        [ForeignKey("RoleId")]
        public virtual AplicationRole Role { get; set; }

        [ForeignKey("PermisionId")]
        public virtual Permision Permision { get; set; }
        
    }

    public class AplicationRole: IdentityRole
    {
        public virtual ICollection<RolePermision> RolePermisions { get; set; }
    }

    public class CustomRolePrivider : RoleProvider
    {
        public override string ApplicationName {
            get {throw new NotImplementedException(); }
            set { throw new NotImplementedException(); } }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {

            
            if (!RoleExists(roleName))
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                    var role = new IdentityRole()
                    {
                        Name = roleName
                    };
                    roleManager.Create(role);
                }
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user =
                    db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username, 
                    StringComparison.CurrentCultureIgnoreCase) || u.Email.Equals(username, StringComparison.CurrentCultureIgnoreCase)).GetAwaiter().GetResult();
                
                var roles = user.Roles;
                List<String> list_roles = new List<String>();
                foreach (var role in roles)
                {
                    var permisions = db.RolePermisions;
                    foreach(var permision in  permisions.ToListAsync().GetAwaiter().GetResult())
                    {
                        if (permision.RoleId == role.RoleId)
                        {
                            list_roles.Add(permision.Permision.Name);
                        }
                    }
                }
                if (roles != null)
                    return list_roles.ToArray();
                else
                    return new string[] { }; ;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool is_inrole = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user =
                    db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username,
                    StringComparison.CurrentCultureIgnoreCase) || u.Email.Equals(username, StringComparison.CurrentCultureIgnoreCase)).GetAwaiter().GetResult();

                var roles = user.Roles;
                List<String> list_roles = new List<String>();
                foreach (var role in roles)
                {
                    var permisions = db.RolePermisions;
                    foreach (var permision in permisions)
                    {
                        if (permision.RoleId == role.RoleId)
                        {
                            if (permision.Permision.Name == roleName)
                                is_inrole = true;
                        }
                    }
                }
            }
            return is_inrole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                return roleManager.RoleExists(roleName);
            }
        }

        public void CreatePermision(string permisionName, string permisionDescription)
        {
            Permision permision = new Permision()
            {
                Name = permisionName,
                Description = permisionDescription
            };
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Permisions.Add(permision);
                db.SaveChanges();
            }
        }
        public void CreatePermision(string permisionDescription)
        {
            Permision permision = new Permision()
            {
                Name = "Implementar alguna rutina que genere el codigo de manera aleatoria",
                Description = permisionDescription
            };
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Permisions.Add(permision);
                db.SaveChanges();
            }
        }

        public void AddPermisionToRole(string permisionName, string roleName)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var role = db.Roles
                    .FirstOrDefaultAsync(
                    r => r.Name.Equals(
                        roleName, 
                        StringComparison.CurrentCultureIgnoreCase)
                    ).GetAwaiter().GetResult();

                var permision = db.Permisions.FirstOrDefaultAsync(
                        p => p.Name.Equals(
                                permisionName,
                                StringComparison.CurrentCultureIgnoreCase
                            )
                    ).GetAwaiter().GetResult();

                if(role!=null & permision != null)
                {
                    RolePermision rp = new RolePermision()
                    {
                        PermisionId=permision.Id,
                        RoleId=role.Id
                    };
                    db.RolePermisions.Add(rp);
                    db.SaveChanges();
                }
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Permision> Permisions { get; set; }
        public DbSet<RolePermision> RolePermisions { get; set; }

        public ApplicationDbContext()
            : base("TiendaConectionString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermision>().HasKey(r => new { r.RoleId, r.PermisionId});
            base.OnModelCreating(modelBuilder);
        }
    }
}