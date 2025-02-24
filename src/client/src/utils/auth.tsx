import React, {
  createContext,
  useState,
  useEffect,
  useContext,
  ReactNode,
} from "react";
import {
  QueryObserverResult,
  RefetchOptions,
  useQuery,
} from "@tanstack/react-query";
import { User } from "@/types/User";
import { fetchRefreshTokenNewSession } from "./api";
import axios from "axios";

// Define the context type
interface AuthContextType {
  user: User | null;
  setUser: React.Dispatch<React.SetStateAction<User | null>>;
  refreshToken: (options?: RefetchOptions | undefined) => Promise<
    QueryObserverResult<
      {
        id: string;
        username: string;
        email: string | null;
        isAdmin: boolean;
      },
      Error
    >
  >;
}

// Create the AuthContext
const AuthContext = createContext<AuthContextType | undefined>(undefined);

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);

  // like useEffect [], where we attempt to get the jwtToken using refreshToken.
  const { isError, error, refetch } = useQuery({
    queryKey: ["refresh"],
    queryFn: async () => {
      const data = await fetchRefreshTokenNewSession();
      setUser(data);
      localStorage.setItem("user", JSON.stringify(data));
      console.log("Used refresh token to log in as ", data.username);
      return data;
    },
    enabled: true,
    retry: false,
    staleTime: Infinity,
  });

  const value: AuthContextType = {
    user,
    setUser,
    refreshToken: refetch, // only used by testing/auth
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

// Custom hook for consuming the AuthContext
export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
