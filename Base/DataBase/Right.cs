using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Base.DataBase
{
    public partial class Right
    {
        public static List<Right> GetRights()
        {
            List<Right> result;
            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                result = context.Rights.ToList();
            }
            return result;
        }

        public static List<Group> GetGroups()
        {
            List<Group> result;
            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<Group>(u => u.UserEntityRights);
            loadOptions.LoadWith<UserEntityRight>(u => u.Right);

            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                context.ObjectTrackingEnabled = false;
                context.LoadOptions = loadOptions;
                result = context.Groups.ToList();
            }
            return result;
        }

        public static List<Entity> GetEntities()
        {
            List<Entity> result;
            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<Entity>(u => u.EntityType);
            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                context.ObjectTrackingEnabled = false;
                context.LoadOptions = loadOptions;
                result = context.Entities.OrderBy(u => u.EntityName).ToList();
            }
            return result;
        }

        public static List<Module> GetModules()
        {
            List<Module> result;
            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<Module>(d => d.ModuleEntities);
            loadOptions.LoadWith<ModuleEntity>(de => de.Entity);
            loadOptions.LoadWith<Entity>(e => e.EntityType);

            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                context.ObjectTrackingEnabled = false;
                context.LoadOptions = loadOptions;
                result = context.Modules.ToList();
            }
            return result;
        }

        public static Group GetGroup(int groupId)
        {
            Group result;
            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<Group>(u => u.UserEntityRights);
            loadOptions.LoadWith<UserEntityRight>(u => u.Right);

            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                context.ObjectTrackingEnabled = false;
                context.LoadOptions = loadOptions;
                result = context.Groups.First(u => u.Group_ID == groupId);
            }
            return result;
        }

        public static int GetFirstRight()
        {
            try
            {
                int result;

                using (RightsLinqDataContext context = new RightsLinqDataContext())
                {
                    context.ObjectTrackingEnabled = false;
                    result = context.Rights.First().RightId;
                }
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Nu aveti definit nici un drept in tabela Rights");
            }
        }

        public static List<User> GetUsers()
        {
            List<User> result;
            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<User>(u => u.Group);
            loadOptions.LoadWith<User>(u => u.Filiale);
            loadOptions.LoadWith<User>(u => u.Firme_Persoane_Contact);

            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                context.ObjectTrackingEnabled = false;
                context.LoadOptions = loadOptions;
                result = context.Users.ToList();
            }
            return result;
        }

        public static List<EntityType> GetEntityTypes()
        {
            List<EntityType> result;
            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                context.ObjectTrackingEnabled = false;
                result = context.EntityTypes.ToList();
            }
            return result;
        }


        public static bool SaveEntity(string entityName, string entityDescription,int entityType, int moduleEntity)
        {
            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                try
                {
                    Entity e = new Entity();
                    e.EntityName = entityName;
                    e.Description = entityDescription;
                    e.EntityTypeId = entityType;
                    ModuleEntity me = new ModuleEntity();
                    me.ModuleId = moduleEntity;
                    e.ModuleEntities.Add(me);
                    context.Entities.InsertOnSubmit(e);
                    context.SubmitChanges();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public static bool SaveModuleEntity(string moduleName)
        {
            using (RightsLinqDataContext context = new RightsLinqDataContext())
            {
                try
                {
                    Module e = new Module();
                    e.ModuleName = moduleName;
                    context.Modules.InsertOnSubmit(e);
                    context.SubmitChanges();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

    }
}