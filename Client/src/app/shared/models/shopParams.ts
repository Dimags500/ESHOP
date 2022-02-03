/**
 * this class send to shop.service and resposble for param for query string 
 * that shop.service will request from API
 */

export class ShopParams{
    brandId = 0;
    typeId = 0;
    sort = 'name';
    pageNumber = 1;
    pageSize = 6 ;
    search : string ;
}