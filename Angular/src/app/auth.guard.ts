import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Observable } from "rxjs";
import { AutentificareService } from "./autentificare.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {
    constructor(private router: Router, private autentificateService: AutentificareService) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
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
