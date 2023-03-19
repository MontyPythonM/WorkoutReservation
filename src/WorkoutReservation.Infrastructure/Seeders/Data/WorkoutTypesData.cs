﻿using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Infrastructure.Seeders.Data;

internal sealed class WorkoutTypesData
{
    private const string HathaYogaDescription = "The purpose of Hatha Yoga is relief from three types of pain — physical, environmental and spiritual, " +
                                                "and through the practices giving in the Hatha Yoga Pradipika, we are able to prepare the body for the Raja Yoga.";
    private const string VinyasaYogaDescription = "Vinyasa is a style of yoga characterized by stringing postures together so that you move from one to another, seamlessly, using breath. " +
                                                  "Commonly referred to as “flow” yoga, it is sometimes confused with “power yoga“. Vinyasa classes offer a variety of postures and no two classes are ever alike.";
    private const string CrossfitDescription = "A form of high intensity interval training, CrossFit is a strength and conditioning workout that is made up of functional movement performed at a high intensity level. " +
                                               "These movements are actions that you perform in your day-to-day life, like squatting, pulling, pushing etc.";
    private const string FullBodyWorkoutDescription = "A full body workout is just what it sounds like: a workout that aims to hit all the major muscle groups in one single session. " +
                                                      "Popular programs would include exercises for back, legs, chest, shoulders, arms and core.";
    
    internal static IEnumerable<WorkoutType> Create()
    {
        var hathaYogaTags = new List<WorkoutTypeTag>
        {
            new("Relax"), 
            new("Cosmic power"), 
            new("Self control"), 
            new("Stretching")
        };
        
        var crossfitTags = new List<WorkoutTypeTag>
        { 
            new("Strength"), 
            new("Vigor"),
            new("Fat melting") 
        };
        
        var hathaYoga = new WorkoutType("Hatha yoga", HathaYogaDescription, WorkoutIntensity.Low, new List<Instructor>(), hathaYogaTags);
        var crossfit = new WorkoutType("Crossfit", CrossfitDescription, WorkoutIntensity.Extreme, new List<Instructor>(), crossfitTags);
        
        var workoutTypes = new List<WorkoutType>
        {
            new("Vinyasa yoga", VinyasaYogaDescription, WorkoutIntensity.Moderate, new List<Instructor>(), new List<WorkoutTypeTag>()),
            new("Full-body workout", FullBodyWorkoutDescription, WorkoutIntensity.Vigorous, new List<Instructor>(), new List<WorkoutTypeTag>()),
            hathaYoga,
            crossfit
        };
        
        return workoutTypes;
    }
}
