export function formatDate(date: Date) {
  return new Intl.DateTimeFormat('da-DK', {
    dateStyle: 'short'
  }).format(date)
}
