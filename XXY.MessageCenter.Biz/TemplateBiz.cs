﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common.Attributes;
using XXY.Common.Exceptions;
using XXY.MessageCenter.BizEntity;
using XXY.MessageCenter.DbContext;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.Common.Extends;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.IBiz;

namespace XXY.MessageCenter.Biz {

    [AutoInjection(typeof(ITemplate))]
    public class TemplateBiz : BaseBiz, ITemplate {
        public Template GetByCode(string code, string appCode, Langs? lang) {
            code = code.ToUpper().Trim();
            appCode = appCode.ToUpper();

            using (var db = new Entities()) {
                var query = db.Templates.Where(t =>
                    !t.IsDeleted
                    && t.Code.ToUpper() == code
                    && t.AppCode.ToUpper() == appCode
                    && ((lang != null && t.Lang == lang.Value) || (lang == null && t.IsDefault))
                    );

                return query.FirstOrDefault();
            }
        }

        public async Task Create(Template entry) {
            var code = entry.Code.ToUpper();
            var appCode = entry.AppCode.ToUpper();
            var lang = entry.Lang;

            using (var db = new Entities()) {
                if (this.IsRepeat(db, entry.Code, entry.AppCode, entry.Lang))
                    throw new DataRepeatException<Template>(entry, t => t.Code, t => t.AppCode, t => t.Lang);
                else {
                    var template = new Template() {
                        Code = code,
                        AppCode = appCode,
                        Lang = lang,
                        Ctx = entry.Ctx,
                        Subject = entry.Subject,
                        IsDefault = entry.IsDefault
                    };
                    this.SetCreateInfo(template);
                    db.Templates.Add(template);
                    this.Errors = db.GetErrors();
                    if (!this.HasError)
                        await db.SaveChangesAsync();
                }
            }
        }


        public async Task Edit(int id, Template entry) {
            using (var db = new Entities()) {
                if (!this.IsRepeat(db, entry.Code, entry.AppCode, entry.Lang, id)) {
                    var ex = db.Templates.FirstOrDefault(t => t.ID == id && !t.IsDeleted);
                    if (ex != null) {
                        entry.CopyToOnly(ex,
                            p => p.Code, p => p.AppCode, p => p.Lang,
                            p => p.Ctx, p => p.Subject,
                            p => p.IsDefault
                            );
                        this.SetModifyInfo(ex);
                        this.Errors = db.GetErrors();
                        if (!this.HasError)
                            await db.SaveChangesAsync();
                    }
                } else {
                    throw new DataRepeatException<Template>(entry, t => t.AppCode, t => t.Code, t => t.Lang);
                }
            }
        }

        public Template GetBySeq(decimal id) {
            using (var db = new Entities()) {
                return db.Templates.FirstOrDefault(t => t.ID == id);
            }
        }

        public async Task<bool> Delete(decimal id) {
            using (var db = new Entities()) {
                var ex = db.Templates.FirstOrDefault(t => t.ID == id && !t.IsDeleted);
                if (ex != null) {
                    ex.IsDeleted = true;
                    this.SetModifyInfo(ex);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }


        public IEnumerable<Template> Search(TemplateSearchCondition cond) {
            using (var db = new Entities()) {
                var query = cond.Filter(db.Templates.Where(t => !t.IsDeleted));
                return query
                    .OrderBy(m => m.Code)
                    .DoPage(cond.Pager).ToList();
            }
        }

        private bool IsRepeat(Entities db, string code, string appCode, Langs lang, int? id = null) {
            id = id ?? -1;
            code = code.ToUpper().Trim();
            appCode = appCode.ToUpper().Trim();

            return db.Templates.Any(t =>
                !t.IsDeleted
                && t.ID != id

                && t.Code.ToUpper() == code
                && t.AppCode.ToUpper() == appCode
                && t.Lang == lang);
        }

    }
}
