import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AutentificareService } from "./autentificare.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {
    constructor(private router: Router, private autentificateService: AutentificareService) {}

    canActivate(): boolean {
        return this.verifAutentificare();
    }

    private verifAutentificare(): boolean {
        if(this.autentificateService.verifAutentificare()){
            return true;
        } else {
            this.router.navigate(['/login']);
            return false;
        }
    }
}
