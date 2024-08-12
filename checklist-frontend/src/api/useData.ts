import { useEffect, useState } from 'react'

export function useData<T>(fetcher: () => Promise<T>) {
  const [data, setData] = useState<T>()
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    fetcher().then((data) => {
      setData(data)
      setLoading(false)
    })
  }, [])

  return { data, loading }
}
