import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQueryWithErrorHandling } from "../../app/api/baseApi";
import { Product } from "../../app/model/Product";
import { ProductParams } from "../../app/model/productParams";
import { filterEmptyValues } from "../../lib/util";
import { Pagination } from "../../app/model/pagination";
export const catalogApi = createApi({
  reducerPath: "catalogApi",
  baseQuery: baseQueryWithErrorHandling,
  endpoints: (builder) => ({
    fetchProducts: builder.query<
      { items: Product[]; pagination: Pagination },
      ProductParams
    >({
      query: (productParams) => {
        return {
          url: "product",
          params: filterEmptyValues(productParams),
        };
      },
      transformResponse: (items: Product[], meta) => {
        const paginationHeader = meta?.response?.headers.get("Pagination");
        const pagination = paginationHeader
          ? JSON.parse(paginationHeader)
          : null;
        return { items, pagination };
      },
    }),
    fetchProductDetails: builder.query<Product, number>({
      query: (id) => ({ url: `product/${id}` }),
    }),

    fetchFilters: builder.query<{ brands: string[]; types: string[] }, void>({
      query: () => "product/filters",
    }),
  }),
});

export const {
  useFetchProductsQuery,
  useFetchProductDetailsQuery,
  useFetchFiltersQuery,
} = catalogApi;
