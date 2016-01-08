using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SWNAdmin.Utility
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class skill_list
        {
            public static skill_list Load()
            {
                XmlSerializer ser = new XmlSerializer(typeof(skill_list));
                skill_list sl;
                using (XmlReader reader = XmlReader.Create(@"C:\Test\Test\BasicSet.xml"))
                {
                sl = (skill_list)ser.Deserialize(reader);
                }
            return sl;
            }
            private object[] itemsField;

            private string unique_idField;

            private byte versionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("skill", typeof(skill_listSkill))]
            [System.Xml.Serialization.XmlElementAttribute("technique", typeof(skill_listTechnique))]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string unique_id
            {
                get
                {
                    return this.unique_idField;
                }
                set
                {
                    this.unique_idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkill
        {

            private string nameField;

            private byte encumbrance_penalty_multiplierField;

            private bool encumbrance_penalty_multiplierFieldSpecified;

            private string specializationField;

            private object tech_levelField;

            private string difficultyField;

            private byte pointsField;

            private string referenceField;

            private string notesField;

            private string[] categoriesField;

            private skill_listSkillWeapon_bonus[] weapon_bonusField;

            private skill_listSkillPrereq_list prereq_listField;

            private skill_listSkillDefault[] defaultField;

            private byte versionField;

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public byte encumbrance_penalty_multiplier
            {
                get
                {
                    return this.encumbrance_penalty_multiplierField;
                }
                set
                {
                    this.encumbrance_penalty_multiplierField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool encumbrance_penalty_multiplierSpecified
            {
                get
                {
                    return this.encumbrance_penalty_multiplierFieldSpecified;
                }
                set
                {
                    this.encumbrance_penalty_multiplierFieldSpecified = value;
                }
            }

            /// <remarks/>
            public string specialization
            {
                get
                {
                    return this.specializationField;
                }
                set
                {
                    this.specializationField = value;
                }
            }

            /// <remarks/>
            public object tech_level
            {
                get
                {
                    return this.tech_levelField;
                }
                set
                {
                    this.tech_levelField = value;
                }
            }

            /// <remarks/>
            public string difficulty
            {
                get
                {
                    return this.difficultyField;
                }
                set
                {
                    this.difficultyField = value;
                }
            }

            /// <remarks/>
            public byte points
            {
                get
                {
                    return this.pointsField;
                }
                set
                {
                    this.pointsField = value;
                }
            }

            /// <remarks/>
            public string reference
            {
                get
                {
                    return this.referenceField;
                }
                set
                {
                    this.referenceField = value;
                }
            }

            /// <remarks/>
            public string notes
            {
                get
                {
                    return this.notesField;
                }
                set
                {
                    this.notesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("category", IsNullable = false)]
            public string[] categories
            {
                get
                {
                    return this.categoriesField;
                }
                set
                {
                    this.categoriesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("weapon_bonus")]
            public skill_listSkillWeapon_bonus[] weapon_bonus
            {
                get
                {
                    return this.weapon_bonusField;
                }
                set
                {
                    this.weapon_bonusField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_list prereq_list
            {
                get
                {
                    return this.prereq_listField;
                }
                set
                {
                    this.prereq_listField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("default")]
            public skill_listSkillDefault[] @default
            {
                get
                {
                    return this.defaultField;
                }
                set
                {
                    this.defaultField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillWeapon_bonus
        {

            private skill_listSkillWeapon_bonusName nameField;

            private skill_listSkillWeapon_bonusSpecialization specializationField;

            private skill_listSkillWeapon_bonusLevel levelField;

            private skill_listSkillWeapon_bonusAmount amountField;

            /// <remarks/>
            public skill_listSkillWeapon_bonusName name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillWeapon_bonusSpecialization specialization
            {
                get
                {
                    return this.specializationField;
                }
                set
                {
                    this.specializationField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillWeapon_bonusLevel level
            {
                get
                {
                    return this.levelField;
                }
                set
                {
                    this.levelField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillWeapon_bonusAmount amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillWeapon_bonusName
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillWeapon_bonusSpecialization
        {

            private string compareField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillWeapon_bonusLevel
        {

            private string compareField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillWeapon_bonusAmount
        {

            private string per_levelField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string per_level
            {
                get
                {
                    return this.per_levelField;
                }
                set
                {
                    this.per_levelField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_list
        {

            private object[] itemsField;

            private string allField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("advantage_prereq", typeof(skill_listSkillPrereq_listAdvantage_prereq))]
            [System.Xml.Serialization.XmlElementAttribute("attribute_prereq", typeof(skill_listSkillPrereq_listAttribute_prereq))]
            [System.Xml.Serialization.XmlElementAttribute("prereq_list", typeof(skill_listSkillPrereq_listPrereq_list))]
            [System.Xml.Serialization.XmlElementAttribute("skill_prereq", typeof(skill_listSkillPrereq_listSkill_prereq))]
            [System.Xml.Serialization.XmlElementAttribute("when_tl", typeof(skill_listSkillPrereq_listWhen_tl))]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string all
            {
                get
                {
                    return this.allField;
                }
                set
                {
                    this.allField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listAdvantage_prereq
        {

            private skill_listSkillPrereq_listAdvantage_prereqName nameField;

            private skill_listSkillPrereq_listAdvantage_prereqNotes notesField;

            private skill_listSkillPrereq_listAdvantage_prereqLevel levelField;

            private string hasField;

            /// <remarks/>
            public skill_listSkillPrereq_listAdvantage_prereqName name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listAdvantage_prereqNotes notes
            {
                get
                {
                    return this.notesField;
                }
                set
                {
                    this.notesField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listAdvantage_prereqLevel level
            {
                get
                {
                    return this.levelField;
                }
                set
                {
                    this.levelField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string has
            {
                get
                {
                    return this.hasField;
                }
                set
                {
                    this.hasField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listAdvantage_prereqName
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listAdvantage_prereqNotes
        {

            private string compareField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listAdvantage_prereqLevel
        {

            private string compareField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listAttribute_prereq
        {

            private string hasField;

            private string whichField;

            private string compareField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string has
            {
                get
                {
                    return this.hasField;
                }
                set
                {
                    this.hasField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string which
            {
                get
                {
                    return this.whichField;
                }
                set
                {
                    this.whichField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_list
        {

            private skill_listSkillPrereq_listPrereq_listPrereq_list[] prereq_listField;

            private skill_listSkillPrereq_listPrereq_listAdvantage_prereq[] advantage_prereqField;

            private skill_listSkillPrereq_listPrereq_listWhen_tl when_tlField;

            private skill_listSkillPrereq_listPrereq_listSkill_prereq[] skill_prereqField;

            private string allField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("prereq_list")]
            public skill_listSkillPrereq_listPrereq_listPrereq_list[] prereq_list
            {
                get
                {
                    return this.prereq_listField;
                }
                set
                {
                    this.prereq_listField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("advantage_prereq")]
            public skill_listSkillPrereq_listPrereq_listAdvantage_prereq[] advantage_prereq
            {
                get
                {
                    return this.advantage_prereqField;
                }
                set
                {
                    this.advantage_prereqField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listPrereq_listWhen_tl when_tl
            {
                get
                {
                    return this.when_tlField;
                }
                set
                {
                    this.when_tlField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("skill_prereq")]
            public skill_listSkillPrereq_listPrereq_listSkill_prereq[] skill_prereq
            {
                get
                {
                    return this.skill_prereqField;
                }
                set
                {
                    this.skill_prereqField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string all
            {
                get
                {
                    return this.allField;
                }
                set
                {
                    this.allField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listPrereq_list
        {

            private skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereq[] skill_prereqField;

            private string allField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("skill_prereq")]
            public skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereq[] skill_prereq
            {
                get
                {
                    return this.skill_prereqField;
                }
                set
                {
                    this.skill_prereqField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string all
            {
                get
                {
                    return this.allField;
                }
                set
                {
                    this.allField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereq
        {

            private skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereqName nameField;

            private skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereqSpecialization specializationField;

            private string hasField;

            /// <remarks/>
            public skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereqName name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereqSpecialization specialization
            {
                get
                {
                    return this.specializationField;
                }
                set
                {
                    this.specializationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string has
            {
                get
                {
                    return this.hasField;
                }
                set
                {
                    this.hasField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereqName
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listPrereq_listSkill_prereqSpecialization
        {

            private string compareField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listAdvantage_prereq
        {

            private skill_listSkillPrereq_listPrereq_listAdvantage_prereqName nameField;

            private skill_listSkillPrereq_listPrereq_listAdvantage_prereqNotes notesField;

            private string hasField;

            /// <remarks/>
            public skill_listSkillPrereq_listPrereq_listAdvantage_prereqName name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listPrereq_listAdvantage_prereqNotes notes
            {
                get
                {
                    return this.notesField;
                }
                set
                {
                    this.notesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string has
            {
                get
                {
                    return this.hasField;
                }
                set
                {
                    this.hasField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listAdvantage_prereqName
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listAdvantage_prereqNotes
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listWhen_tl
        {

            private string compareField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listSkill_prereq
        {

            private skill_listSkillPrereq_listPrereq_listSkill_prereqName nameField;

            private skill_listSkillPrereq_listPrereq_listSkill_prereqSpecialization specializationField;

            private skill_listSkillPrereq_listPrereq_listSkill_prereqLevel levelField;

            private string hasField;

            /// <remarks/>
            public skill_listSkillPrereq_listPrereq_listSkill_prereqName name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listPrereq_listSkill_prereqSpecialization specialization
            {
                get
                {
                    return this.specializationField;
                }
                set
                {
                    this.specializationField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listPrereq_listSkill_prereqLevel level
            {
                get
                {
                    return this.levelField;
                }
                set
                {
                    this.levelField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string has
            {
                get
                {
                    return this.hasField;
                }
                set
                {
                    this.hasField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listSkill_prereqName
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listSkill_prereqSpecialization
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listPrereq_listSkill_prereqLevel
        {

            private string compareField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listSkill_prereq
        {

            private skill_listSkillPrereq_listSkill_prereqName nameField;

            private skill_listSkillPrereq_listSkill_prereqSpecialization specializationField;

            private skill_listSkillPrereq_listSkill_prereqLevel levelField;

            private string hasField;

            /// <remarks/>
            public skill_listSkillPrereq_listSkill_prereqName name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listSkill_prereqSpecialization specialization
            {
                get
                {
                    return this.specializationField;
                }
                set
                {
                    this.specializationField = value;
                }
            }

            /// <remarks/>
            public skill_listSkillPrereq_listSkill_prereqLevel level
            {
                get
                {
                    return this.levelField;
                }
                set
                {
                    this.levelField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string has
            {
                get
                {
                    return this.hasField;
                }
                set
                {
                    this.hasField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listSkill_prereqName
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listSkill_prereqSpecialization
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listSkill_prereqLevel
        {

            private string compareField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillPrereq_listWhen_tl
        {

            private string compareField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listSkillDefault
        {

            private string typeField;

            private string nameField;

            private string specializationField;

            private sbyte modifierField;

            /// <remarks/>
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public string specialization
            {
                get
                {
                    return this.specializationField;
                }
                set
                {
                    this.specializationField = value;
                }
            }

            /// <remarks/>
            public sbyte modifier
            {
                get
                {
                    return this.modifierField;
                }
                set
                {
                    this.modifierField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listTechnique
        {

            private string nameField;

            private string difficultyField;

            private byte pointsField;

            private string referenceField;

            private skill_listTechniqueDefault defaultField;

            private string[] categoriesField;

            private skill_listTechniquePrereq_list prereq_listField;

            private byte versionField;

            private byte limitField;

            private bool limitFieldSpecified;

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public string difficulty
            {
                get
                {
                    return this.difficultyField;
                }
                set
                {
                    this.difficultyField = value;
                }
            }

            /// <remarks/>
            public byte points
            {
                get
                {
                    return this.pointsField;
                }
                set
                {
                    this.pointsField = value;
                }
            }

            /// <remarks/>
            public string reference
            {
                get
                {
                    return this.referenceField;
                }
                set
                {
                    this.referenceField = value;
                }
            }

            /// <remarks/>
            public skill_listTechniqueDefault @default
            {
                get
                {
                    return this.defaultField;
                }
                set
                {
                    this.defaultField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("category", IsNullable = false)]
            public string[] categories
            {
                get
                {
                    return this.categoriesField;
                }
                set
                {
                    this.categoriesField = value;
                }
            }

            /// <remarks/>
            public skill_listTechniquePrereq_list prereq_list
            {
                get
                {
                    return this.prereq_listField;
                }
                set
                {
                    this.prereq_listField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte limit
            {
                get
                {
                    return this.limitField;
                }
                set
                {
                    this.limitField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool limitSpecified
            {
                get
                {
                    return this.limitFieldSpecified;
                }
                set
                {
                    this.limitFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listTechniqueDefault
        {

            private string typeField;

            private string nameField;

            private string specializationField;

            private sbyte modifierField;

            /// <remarks/>
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public string specialization
            {
                get
                {
                    return this.specializationField;
                }
                set
                {
                    this.specializationField = value;
                }
            }

            /// <remarks/>
            public sbyte modifier
            {
                get
                {
                    return this.modifierField;
                }
                set
                {
                    this.modifierField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listTechniquePrereq_list
        {

            private skill_listTechniquePrereq_listSkill_prereq skill_prereqField;

            private string allField;

            /// <remarks/>
            public skill_listTechniquePrereq_listSkill_prereq skill_prereq
            {
                get
                {
                    return this.skill_prereqField;
                }
                set
                {
                    this.skill_prereqField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string all
            {
                get
                {
                    return this.allField;
                }
                set
                {
                    this.allField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listTechniquePrereq_listSkill_prereq
        {

            private skill_listTechniquePrereq_listSkill_prereqName nameField;

            private skill_listTechniquePrereq_listSkill_prereqSpecialization specializationField;

            private string hasField;

            /// <remarks/>
            public skill_listTechniquePrereq_listSkill_prereqName name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public skill_listTechniquePrereq_listSkill_prereqSpecialization specialization
            {
                get
                {
                    return this.specializationField;
                }
                set
                {
                    this.specializationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string has
            {
                get
                {
                    return this.hasField;
                }
                set
                {
                    this.hasField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listTechniquePrereq_listSkill_prereqName
        {

            private string compareField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class skill_listTechniquePrereq_listSkill_prereqSpecialization
        {

            private string compareField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string compare
            {
                get
                {
                    return this.compareField;
                }
                set
                {
                    this.compareField = value;
                }
            }
        }
    }
