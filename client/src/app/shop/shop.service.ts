import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.apiUrl;
  shopParams = new ShopParams();

  constructor(private http: HttpClient) {}

  getProducts(params: ShopParams): Observable<IPagination> {
    let httpParams = new HttpParams();

    if (params.brandId !== 0) {
      httpParams = httpParams.set('brandId', params.brandId.toString());
    }

    if (params.typeId !== 0) {
      httpParams = httpParams.set('typeId', params.typeId.toString());
    }

    if (params.search) {
      httpParams = httpParams.set('search', params.search);
    }

    httpParams = httpParams.set('sort', params.sort);
    httpParams = httpParams.set('pageNumber', params.pageNumber.toString()); 
    httpParams = httpParams.set('pageSize', params.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'products', { params: httpParams });
  }

  setShopParams(params: ShopParams) {
    this.shopParams = params;
  }

  getShopParams() {
    return this.shopParams;
  }

  getProduct(id: number): Observable<IProduct> {
    return this.http.get<IProduct>(`${this.baseUrl}products/${id}`);
  }

  getBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes(): Observable<IType[]> {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
