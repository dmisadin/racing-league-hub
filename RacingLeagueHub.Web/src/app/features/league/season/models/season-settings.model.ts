// Mirrors RacingLeagueHub.Domain.Common.Models.Enums.Assists / .Lobby (serialized as numbers).

export enum RacingLine { Off = 0, CornersOnly = 1, Full = 2 }
export enum Gearbox { Manual = 0, ManualAndSuggestedGear = 1, Automatic = 2 }
export enum TractionControl { Off = 0, Medium = 1, Full = 2 }

export enum QualifyingType { None = 0, OneShot = 1, Short = 2, Full = 3 }
export enum RaceDistance { Quarter = 25, Esport = 35, Half = 50, Full = 100 }
export enum Weather { Dynamic = 0, ClearDry = 1, LightCloudDry = 2, OvercastDry = 3, Wet = 4, VeryWet = 5 }
export enum CornerCuttingStringency { Regular = 0, Strict = 1 }
export enum CarDamage { Off = 0, Reduced = 1, Standard = 2, Simulation = 3 }
export enum CarDamageRate { Reduced = 0, Standard = 1, Simulation = 2 }
export enum SafetyCar { Off = 0, Reduced = 1, Standard = 2, Increased = 3 }
export enum RaceStart { Manual = 0, Assisted = 1 }

export const RacingLineLabels: Record<RacingLine, string> = {
    [RacingLine.Off]: 'Off',
    [RacingLine.CornersOnly]: 'Corners only',
    [RacingLine.Full]: 'Full',
};

export const GearboxLabels: Record<Gearbox, string> = {
    [Gearbox.Manual]: 'Manual',
    [Gearbox.ManualAndSuggestedGear]: 'Manual + suggested gear',
    [Gearbox.Automatic]: 'Automatic',
};

export const TractionControlLabels: Record<TractionControl, string> = {
    [TractionControl.Off]: 'Off',
    [TractionControl.Medium]: 'Medium',
    [TractionControl.Full]: 'Full',
};

export const QualifyingTypeLabels: Record<QualifyingType, string> = {
    [QualifyingType.None]: 'None',
    [QualifyingType.OneShot]: 'One-shot',
    [QualifyingType.Short]: 'Short',
    [QualifyingType.Full]: 'Full',
};

export const RaceDistanceLabels: Record<RaceDistance, string> = {
    [RaceDistance.Quarter]: '25%',
    [RaceDistance.Esport]: '35%',
    [RaceDistance.Half]: '50%',
    [RaceDistance.Full]: '100%',
};

export const WeatherLabels: Record<Weather, string> = {
    [Weather.Dynamic]: 'Dynamic',
    [Weather.ClearDry]: 'Clear (dry)',
    [Weather.LightCloudDry]: 'Light cloud (dry)',
    [Weather.OvercastDry]: 'Overcast (dry)',
    [Weather.Wet]: 'Wet',
    [Weather.VeryWet]: 'Very wet',
};

export const CornerCuttingLabels: Record<CornerCuttingStringency, string> = {
    [CornerCuttingStringency.Regular]: 'Regular',
    [CornerCuttingStringency.Strict]: 'Strict',
};

export const CarDamageLabels: Record<CarDamage, string> = {
    [CarDamage.Off]: 'Off',
    [CarDamage.Reduced]: 'Reduced',
    [CarDamage.Standard]: 'Standard',
    [CarDamage.Simulation]: 'Simulation',
};

export const CarDamageRateLabels: Record<CarDamageRate, string> = {
    [CarDamageRate.Reduced]: 'Reduced',
    [CarDamageRate.Standard]: 'Standard',
    [CarDamageRate.Simulation]: 'Simulation',
};

export const SafetyCarLabels: Record<SafetyCar, string> = {
    [SafetyCar.Off]: 'Off',
    [SafetyCar.Reduced]: 'Reduced',
    [SafetyCar.Standard]: 'Standard',
    [SafetyCar.Increased]: 'Increased',
};

export const RaceStartLabels: Record<RaceStart, string> = {
    [RaceStart.Manual]: 'Manual',
    [RaceStart.Assisted]: 'Assisted',
};
