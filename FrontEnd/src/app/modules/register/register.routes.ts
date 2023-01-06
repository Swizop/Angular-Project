import { Routes } from "@angular/router";
import { NoauthGuard } from "src/app/guards/noauth.guard";
import { ConstructorComponent } from "./constructor/constructor.component";
import { LayoutComponent } from "./layout/layout.component";
import { UserComponent } from "./user/user.component";

export const routes: Routes = [ 
    {
        path: "register",
        component: LayoutComponent,
        canActivate: [NoauthGuard],
        children: [
            { path: '', redirectTo: "user", pathMatch: "full"},
            {
                path: "user", 
                component: UserComponent
            },
            {
                path: "constructor",
                component: ConstructorComponent
            }
        ]
    }
]
