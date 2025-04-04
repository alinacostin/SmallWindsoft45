using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DataBase
{
    public class ModuleEntitiesTreeRecord
    {
        private string fName;
        private int fModuleId;
        private int fId;
        private int fParentID;
        private int fEntityId;

        public ModuleEntitiesTreeRecord(string name, int moduleId, int id) : this(name, -1, moduleId, id, -1) { }
        public ModuleEntitiesTreeRecord(string name, int entityId, int moduleId, int id, int parentId)
        {
            fName = name;
            fEntityId = entityId;
            fModuleId = moduleId;
            fId = id;
            fParentID = parentId;
        }

        public int ID
        {
            get { return fId; }
        }

        public int EntityId() { return fEntityId; }
        public int ModuleId() { return fModuleId; }

        public int ParentID
        {
            get { return fParentID; }
        }

        public string Name
        {
            get { return fName; }
        }

    }
}
