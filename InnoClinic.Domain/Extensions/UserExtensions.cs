using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Extensions
{
    public static class UserExtensions
    {
        private const int NameMatchWeight = 5;
        private const int DateMatchWeight = 3;
        private const int MatchLimit = 13;

        public static bool Matches(this User user, PatientProfileDto patientProfile)
        {
            var coefficient = 0;
            if (patientProfile.FirstName.Equals(user.FirstName))
            {
                coefficient += NameMatchWeight;
            }

            if (patientProfile.LastName.Equals(user.LastName))
            {
                coefficient += NameMatchWeight;
            }

            if (patientProfile.MiddleName.Equals(user.MiddleName))
            {
                coefficient += NameMatchWeight;
            }

            if (patientProfile.DateOfBirth.Equals(user.DateOfBirth))
            {
                coefficient += DateMatchWeight;
            }

            return coefficient >= MatchLimit;
        }
    }
}
