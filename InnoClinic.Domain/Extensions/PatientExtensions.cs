using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Extensions
{
    public static class PatientExtensions
    {
        private const int NameMatchWeight = 5;
        private const int DateMatchWeight = 3;
        private const int MatchLimit = 13;

        public static bool Matches(this Patient patient, PatientProfileDto patientProfile)
        {
            var coefficient = 0;
            if (patientProfile.FirstName.Equals(patient.FirstName))
            {
                coefficient += NameMatchWeight;
            }

            if (patientProfile.LastName.Equals(patient.LastName))
            {
                coefficient += NameMatchWeight;
            }

            if (patientProfile.MiddleName.Equals(patient.MiddleName))
            {
                coefficient += NameMatchWeight;
            }

            if (patientProfile.DateOfBirth.Equals(patient.DateOfBirth))
            {
                coefficient += DateMatchWeight;
            }

            return coefficient >= MatchLimit;
        }
    }
}
