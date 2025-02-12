import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RandevuBilgileriComponent } from './randevu-bilgileri/randevu-bilgileri.component';
import { RandevuOlusturComponent } from './randevu-olustur/randevu-olustur.component';
import { TumRandevuBilgileriComponent } from './tum-randevu-bilgileri/tum-randevu-bilgileri.component';
import { AdminGirisSayfasiComponent } from './admin-giris-sayfasi/admin-giris-sayfasi.component';
 export const routes: Routes = [
    {
        path: '', 
        component: LoginComponent
    },
    {
        path: 'login', 
        component: LoginComponent
    },
    {
        path:'register',
        component:RegisterComponent
    },
    {
        path:'admin',
        component:AdminGirisSayfasiComponent
    },
    {
        path: 'home', 
        component: HomeComponent,
        children:[ 
            {
                path:'randevuOlustur',
                component:RandevuOlusturComponent
            },
            
            {
                path:'randevuBilgileri',
                component:RandevuBilgileriComponent
            },
            {
                path:'randevularim',
                component:TumRandevuBilgileriComponent
            }
            

        ]
       
    }
   
];
