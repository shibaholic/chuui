import { api_url } from "@/utils/api";
import { Button } from "@/components/ui/button";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";

class WeatherForecast {
  date: string;
  summary: string;
  temperatureC: number;
  temperatureF: number;

  toString(): string {
    return `${this.date} ${this.summary} ${this.temperatureC} C`;
  }
}

const WeatherTestPage = () => {
  const [weatherForecastData, setWeatherForecastData] = useState<string>("");

  // Access the client
  const queryClient = useQueryClient();

  // Queries
  const { data, refetch, isError } = useQuery({
    queryKey: ["weatherforecast"],
    queryFn: async () => {
      let result = await fetch(`${api_url}/WeatherForecast`);
      if (!result.ok) throw new Error("Network response was not ok");
      return result.json();
    },
    enabled: false,
  });

  async function weatherForecastAction() {
    const freshData = await refetch();
    // const { isPending, isError, data, error, refetch } = weatherForecastQuery;

    if (isError) setWeatherForecastData("error");

    const newData = freshData.data;

    console.debug(newData);

    let weatherForecastParsed = new WeatherForecast();
    Object.assign(weatherForecastParsed, newData[0]);

    console.debug(weatherForecastParsed);
    console.log(weatherForecastParsed.toString());

    setWeatherForecastData(weatherForecastParsed.toString());
  }

  return (
    <div>
      <Button onClick={() => weatherForecastAction()}>
        GET /WeatherForecast
      </Button>
      {weatherForecastData ? <p>{weatherForecastData}</p> : <p>nothing</p>}
    </div>
  );
};

export default WeatherTestPage;
