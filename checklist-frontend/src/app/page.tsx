import { Box } from '@mui/material'
import { Session } from 'next-auth'
import NavigationBar from '../components/navigation/NavigationBar'
import { QuickCreate } from '../components/navigation/QuickCreate'
import HomePage from './HomePage'
import { auth } from '@/auth'
import { SessionProvider } from 'next-auth/react'

interface Props {
  session: Session | null
}
export default async function Home(props: any) {
  const session = await auth()
  console.log('session', session)
  return (
    <Box sx={{ minHeight: '100svh' }}>
      user: {session?.user?.email}
      <NavigationBar title="Checklists" />
      <HomePage session={props.session} />
      <QuickCreate />
    </Box>
  )
}

// export async function getServerSideProps(context: GetServerSidePropsContext) {
//   const session = await getServerSession(context.req, context.res, config)
//   return {
//     props: {
//       session: session
//     }
//   }
// }
