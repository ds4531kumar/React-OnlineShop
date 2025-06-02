import { BaseQueryApi, FetchArgs } from "@reduxjs/toolkit/query";
import { fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { startLoading, stopLoading } from "../layout/uiSlice";
import { toast } from "react-toastify";
import { router } from "../router/Routes";

const customBaseQuery = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_API_URL,
    credentials: "include",
});
type ErrorResponse = string | { title: string } | { errors: string[] };

const sleep = () => new Promise((resolve) => setTimeout(resolve, 1000));

export const baseQueryWithErrorHandling = async (
    args: string | FetchArgs,
    api: BaseQueryApi,
    extraOptions: object
) => {
    api.dispatch(startLoading());
    await sleep();
    const result = await customBaseQuery(args, api, extraOptions);
    api.dispatch(stopLoading());
    if (result.error) {
        const { status, data } = result.error;
        console.log("Error", status, data);
        const originalStatus =
            result.error.status == "PARSING_ERROR" &&
            result.error.originalStatus
                ? result.error.originalStatus
                : result.error.status;

        console.log(originalStatus);
        const responseData = result.error.data as ErrorResponse;
        console.log(responseData);
        switch (originalStatus) {
            case 400:
                if (typeof responseData === "string") toast.error(responseData);
                else if ("errors" in responseData) {
                    throw Object.values(responseData.errors).flat().join(", ");
                } else toast.error(responseData.title);
                break;
            case 401:
                if (typeof responseData === "object" && "title" in responseData)
                    toast.error(responseData.title);
                else toast.error(responseData as string);
                break;
            case 404:
                router.navigate("/not-found");
                break;
            case 500:
                router.navigate("/server-error", {
                    state: { error: responseData },
                });
                break;
            default:
                toast.error("Something went wrong");
                break;
        }
    }
    return result;
};
