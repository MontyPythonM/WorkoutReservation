using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders
{
    internal class DummyWorkoutTypes
    {
        internal static List<WorkoutType> GetWorkoutTypes()
        {
            var workoutTypes = new List<WorkoutType>
            {
                new WorkoutType()
                {
                    Name = "Hatha yoga",
                    Description = "The purpose of Hatha Yoga is relief from three types of pain — physical, environmental and spiritual, and through the practices giving in the Hatha Yoga Pradipika, we are able to prepare the body for the Raja Yoga.",
                    Intensity = WorkoutIntensity.low
                },

                new WorkoutType()
                {
                    Name = "Vinyasa yoga",
                    Description = "Vinyasa is a style of yoga characterized by stringing postures together so that you move from one to another, seamlessly, using breath. Commonly referred to as “flow” yoga, it is sometimes confused with “power yoga“. Vinyasa classes offer a variety of postures and no two classes are ever alike.",
                    Intensity = WorkoutIntensity.moderate
                },

                new WorkoutType()
                {
                    Name = "Crossfit",
                    Description = "A form of high intensity interval training, CrossFit is a strength and conditioning workout that is made up of functional movement performed at a high intensity level. These movements are actions that you perform in your day-to-day life, like squatting, pulling, pushing etc.",
                    Intensity = WorkoutIntensity.extreme
                },

                new WorkoutType()
                {
                    Name = "Full-body workout",
                    Description = "A full body workout is just what it sounds like: a workout that aims to hit all the major muscle groups in one single session. Popular programs would include exercises for back, legs, chest, shoulders, arms and core.",
                    Intensity = WorkoutIntensity.vigorous
                }
            };

            return workoutTypes;
        }
    }
}
