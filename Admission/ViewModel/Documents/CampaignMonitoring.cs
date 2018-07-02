using CommonMethods.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Spreadsheet;
using ResourceLibrary.Documents;
using Model.Admission;
using MoreLinq;

namespace Admission.ViewModel.Documents
{
    public class CampaignMonitoring : OpenXmlDocument
    {
        public override void CreatePackage(string fileName)
        {
            var ef = ExcelFile.Load(DocumentTemplate.ExtractXls("CampaignMonitoring"));
            var ws = ef.Worksheets[0];

            // копируем 6 строку как шаблон
            int i = 6;
            var rowTemplate = ws.Rows[i];

            // получаем направления из конкурсных групп
            var directions = (from compGroup in Session.DataModel.CompetitiveGroups
                                where compGroup.Campaign.CampaignStatusId == 2 &&
                                compGroup.EducationLevel.Id == 1
                                select compGroup.Direction).ToList();
            directions = directions.Distinct().OrderBy(d => d.Code).ToList();

            // собсно, выгрузка данных
            int kcpTotal = 0, kcpO = 0, kcpZ = 0, kcpPaid = 0;
            int claimsTotal = 0, claimsO = 0, claimsZ = 0, claimsPaid = 0;
            foreach (var dir in directions)
            {
                kcpTotal = dir.CompetitiveGroups
                    .Where(cc => cc.Campaign.CampaignStatusId == 2)
                    .Sum(cc => cc.PlaceCount) ?? 0;
                kcpO = dir.CompetitiveGroups
                    .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.EducationFormId == 1)
                    .Sum(cc => cc.PlaceCount) ?? 0;
                kcpZ = dir.CompetitiveGroups
                    .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.EducationFormId == 2)
                    .Sum(cc => cc.PlaceCount) ?? 0;
                kcpPaid = dir.CompetitiveGroups
                    .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.FinanceSourceId == 2)
                    .Sum(cc => cc.PlaceCount) ?? 0;
                claimsTotal = dir.CompetitiveGroups
                    .Where(cc => cc.Campaign.CampaignStatusId == 2)
                    .SelectMany(cc => cc.ClaimConditions)
                    .Distinct()
                    .Count();
                claimsO = dir.CompetitiveGroups
                    .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.EducationFormId == 1)
                    .SelectMany(cc => cc.ClaimConditions)
                    .Distinct()
                    .Count();
                claimsZ = dir.CompetitiveGroups
                    .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.EducationFormId == 2)
                    .SelectMany(cc => cc.ClaimConditions)
                    .Distinct()
                    .Count();
                claimsPaid = dir.CompetitiveGroups
                    .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.FinanceSourceId == 2)
                    .SelectMany(cc => cc.ClaimConditions)
                    .Distinct()
                    .Count();

                // ВЫгрузка в документ
                ws.Rows.InsertCopy(i, rowTemplate);
                var currentRow = ws.Rows[i];
                i++;

                // Вставляем данные
                currentRow.Cells[0].Value = dir.Name;
                currentRow.Cells[1].Value = kcpTotal;
                currentRow.Cells[2].Value = kcpO;
                currentRow.Cells[3].Value = claimsO;
                currentRow.Cells[4].Value = kcpZ;
                currentRow.Cells[5].Value = claimsZ;
                currentRow.Cells[10].Value = claimsPaid;
                currentRow.Cells[11].Value = claimsTotal;
            }

            ws.Rows[i].Hidden = true;

            // подсчет общих значений по столбцам
            int totalKcp = 0, totalKcpO = 0, totalKcpZ = 0,
                totalClaimsO = 0, totalClaimsZ = 0, totalClaimsPaid = 0,
                totalClaims = 0;

            totalKcp = directions
                .SelectMany(d => d.CompetitiveGroups)
                .Where(cc => cc.Campaign.CampaignStatusId == 2)
                .Sum(cc => cc.PlaceCount ?? 0);
            totalKcpO = directions
                .SelectMany(d => d.CompetitiveGroups)
                .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.EducationFormId == 1)
                .Sum(cc => cc.PlaceCount ?? 0);
            totalKcpZ = directions
                .SelectMany(d => d.CompetitiveGroups)
                .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.EducationFormId == 2)
                .Sum(cc => cc.PlaceCount ?? 0);
            totalClaims = directions
                .SelectMany(d => d.CompetitiveGroups)
                .Where(cc => cc.Campaign.CampaignStatusId == 2)
                .SelectMany(cc => cc.ClaimConditions).Distinct()
                .Count();
            totalClaimsO = directions
                .SelectMany(d => d.CompetitiveGroups)
                .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.EducationFormId == 1)
                .SelectMany(cc => cc.ClaimConditions).Distinct()
                .Count();
            totalClaimsZ = directions
                .SelectMany(d => d.CompetitiveGroups)
                .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.EducationFormId == 2)
                .SelectMany(cc => cc.ClaimConditions).Distinct()
                .Count();
            totalClaimsPaid = directions
                .SelectMany(d => d.CompetitiveGroups)
                .Where(cc => cc.Campaign.CampaignStatusId == 2 && cc.FinanceSourceId == 2)
                .SelectMany(cc => cc.ClaimConditions).Distinct()
                .Count();

            ws.Rows[5].Cells[1].Value = totalKcp;
            ws.Rows[5].Cells[2].Value = totalKcpO;
            ws.Rows[5].Cells[3].Value = totalClaimsO;
            ws.Rows[5].Cells[4].Value = totalKcpZ;
            ws.Rows[5].Cells[5].Value = totalClaimsZ;
            ws.Rows[5].Cells[10].Value = totalClaimsPaid;
            ws.Rows[5].Cells[11].Value = totalClaims;

            ef.Save(fileName);
        }
    }
}
