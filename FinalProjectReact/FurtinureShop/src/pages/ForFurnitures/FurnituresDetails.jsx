import { useParams } from "react-router-dom";
import Furniture from "./Furniture.jsx"
import { useQuery } from "@tanstack/react-query";
import { getFurnitureById } from "../../api/services/furnitureServices.js";

const FurnituresDetails = () => {
  const { id } = useParams();

  const {
    data: furniture,
    isLoading,
    isError,
  } = useQuery({
    queryKey: ["furniture", id],
    queryFn: () => getFurnitureById(id),
    enabled: !!id,
  });

  if (isLoading) return <p>Loading...</p>;
  if (isError) return <p>Error loading furniture</p>;

  return (
    <Furniture furniture={furniture} fromAll={false} />
  );
};

export default FurnituresDetails