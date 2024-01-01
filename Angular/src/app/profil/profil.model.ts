import { Articol } from "../articol/articol.model";

export interface Profil {
    id: number;
    nume: string;
    prenume: string;
    bio: string;
    utilizatorId: number;
    articole: Articol[];
};

export interface PostProfilDto {
    nume: string;
    prenume: string;
    bio: string
}