export interface LeagueRole {
    leagueId: number;
    leagueSlug: string;
    isOwner: boolean;
    isAdmin: boolean;
    isEditor: boolean;
    isSteward: boolean;
}
