using ChatAPI.Interfaces;
using ChatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Services
{
    public class AgentFactoryService : IAgentFactoryService
    {    

        private static List<Team> teams = new List<Team>();

        public AgentFactoryService()
        {
            List<Agent> teamA = new List<Agent>();

            var teamLead = new Agent() { Name = "Rikas", Level = 0.5 , LevelName = "Team Lead" };
            var midLevel1 = new Agent() { Name = "Man", Level = 0.6 , LevelName = "Mid-Level"};
            var midLevel2 = new Agent() { Name = "Rim", Level = 0.6 , LevelName = "Mid-Level"};
            var junior = new Agent() { Name = "Saj", Level = 0.4 , LevelName = "Junior"};


            teamA.Add(teamLead);
            teamA.Add(midLevel1);
            teamA.Add(midLevel2);
            teamA.Add(junior);

            List<Agent> teamB = new List<Agent>();

            var senior = new Agent() { Name = "Rum", Level = 0.8, LevelName = "Senior" };
            var midLevel3 = new Agent() { Name = "Man", Level = 0.6, LevelName = "Mid-Level" };
            var junior1 = new Agent() { Name = "Saj", Level = 0.4, LevelName = "Junior" };
            var junior2 = new Agent() { Name = "Saj1", Level = 0.4, LevelName = "Junior" };

            teamB.Add(senior);
            teamB.Add(midLevel3);
            teamB.Add(junior1);
            teamB.Add(junior2);


            List<Agent> teamC = new List<Agent>();


            var midLevel4 = new Agent() { Name = "Man", Level = 0.6, LevelName = "Mid-Level" };
            var midLevel5 = new Agent() { Name = "Man2", Level = 0.6, LevelName = "Mid-Level" };

            teamC.Add(midLevel4);
            teamC.Add(midLevel5);

            List<Agent> teamOverFlow = new List<Agent>();

            var junior3 = new Agent() { Name = "Raj1", Level = 0.4, LevelName = "Junior" };
            var junior4 = new Agent() { Name = "Raj2", Level = 0.4, LevelName = "Junior" };
            var junior5 = new Agent() { Name = "Raj3", Level = 0.4, LevelName = "Junior" };
            var junior6 = new Agent() { Name = "Raj4", Level = 0.4, LevelName = "Junior" };
            var junior7 = new Agent() { Name = "Raj5", Level = 0.4, LevelName = "Junior" };
            var junior8 = new Agent() { Name = "Raj6", Level = 0.4, LevelName = "Junior" };

            teamOverFlow.Add(junior3);
            teamOverFlow.Add(junior4);
            teamOverFlow.Add(junior5);
            teamOverFlow.Add(junior6);
            teamOverFlow.Add(junior7);
            teamOverFlow.Add(junior8);


            teams.Add(new Team() {Name = "A" , ShiftTime = "8to4" , Members = teamA , Capacity = CalculateCapacity(teamA) });
            teams.Add(new Team() {Name = "B" , ShiftTime = "4to8" , Members = teamA , Capacity = CalculateCapacity(teamB) });
            teams.Add(new Team() {Name = "C" , ShiftTime = "8to12" , Members = teamA, Capacity = CalculateCapacity(teamC) });
            teams.Add(new Team() {Name = "O" , ShiftTime = "8to4" , Members = teamOverFlow , Capacity = CalculateCapacity(teamOverFlow) });



        }


        private double CalculateCapacity(List<Agent> team)
        {
            double count = 0;

            foreach (var t in team)
            {
                count += t.Level * 10;
            }

            return count * 1.5;
        }


        public Team GetCurrentShiftTeam()
        {
            return teams.FirstOrDefault();
        }
    }
}
