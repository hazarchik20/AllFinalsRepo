import React from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { getUsers, putUser } from "../../api/services/userServices";
import { useNavigate } from "react-router-dom";

const UsersAdmin = () => {
  console.log("IN USERS");
  
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const { data: users = [], isLoading, isError } = useQuery({
    queryKey: ["users"],
    queryFn: getUsers,
  });

  const updateUserMutation = useMutation({
    mutationFn: putUser,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
    },
  });

  if (isLoading) return <p>Loading...</p>;
  if (isError) return <p>Error loading users</p>;

  const nonAdminUsers = users.filter(u => !u.isAdmin);

  return (
    <div className="users-admin">
      <h2 className="title">Користувачі</h2>

      <table className="users-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Статус</th>
            <th>Блокування</th>
            <th>Замовлення</th>
          </tr>
        </thead>

        <tbody>
          {nonAdminUsers.map(user => (
            <tr key={user.Id}>
              <td>{user.UserName}</td>
              <td>{user.Email}</td>

              <td>
                {user.IsBlocked ? (
                  <span className="status blocked">Заблоковано</span>
                ) : (
                  <span className="status active">Активний</span>
                )}
              </td>

              <td>
                <input
                  type="checkbox"
                  checked={user.IsBlocked}
                  onChange={() =>
                    updateUserMutation.mutate({
                      ...user,
                      IsBlocked: !user.IsBlocked,
                    })
                  }
                />
              </td>

              <td>
                <button
                  className="btn"
                  onClick={() => navigate(`/orders/${user.Id}`)}
                >
                  Переглянути
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UsersAdmin;