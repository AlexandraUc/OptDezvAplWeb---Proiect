export interface Profil {
    id: number;
    nume: string;
    prenume: string;
    bio: string;
    utilizatorId: number;
};

export interface PostProfilDto {
    nume: string;
    prenume: string;
    bio: string
}