//import React from "react";
//import httpService from "../services/HttpService";

export default function App() {
    //const [data, setData] = React.useState<{ fact: string } | null>(null);
    //const [error, setError] = React.useState<string | null>(null);

    //React.useEffect(() => {
    //    httpService.get<{ fact: string }>("Exercise")
    //        .then(setData)
    //        .catch((err) => setError(err.message));
    //}, []);

    return (
        <div className="flex justify-center items-center">
            <div className="flex flex-col">
                <p className="flex flex-col">XD</p>
                {/*{error && <p>Error: {error}</p>}*/}
                {/*{data ? <div>{data.fact}</div> : <p>Loading...</p>}*/}
            </div>
        </div>
    );
}
