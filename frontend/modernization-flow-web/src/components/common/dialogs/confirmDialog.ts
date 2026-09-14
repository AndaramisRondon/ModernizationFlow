import Swal from 'sweetalert2'

type ConfirmDialogOptions = {
  title: string
  text: string
  confirmText: string
}

export async function confirmDialog({
  title,
  text,
  confirmText,
}: ConfirmDialogOptions) {
  const result = await Swal.fire({
    title,
    text,
    icon: 'question',
    showCancelButton: true,
    confirmButtonText: confirmText,
    cancelButtonText: 'Cancelar',
    reverseButtons: true,
  })

  return result.isConfirmed
}