import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Observable } from "rxjs";
import { Province } from "../../models/province";
import { ProvinceService } from "../../services/province.service";

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']
})
export class ProvinceComponent implements OnInit {
    private sub: any;
    observableProvince: Observable<Province>;
    provinces: Province;
    errorMessage: String;

    constructor(@Inject(ProvinceService) private provinceService) {
    }

    ngOnInit() {
        this.observableProvince = this.provinceService.getProvinces();
        this.observableProvince.subscribe(province => {
            this.provinces = province;
        },
            error => this.errorMessage = <any>error);
  }

}
